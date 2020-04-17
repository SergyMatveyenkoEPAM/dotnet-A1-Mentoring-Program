using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace WebCrawler
{
    public enum AreaRestriction { Unlimited, CurrentDomainOnly, NotHigherSourcePath }

    public class Crawler
    {
        public event EventHandler<OutputMessageEventArgs> TraceMessage;
        public int MaxLayer { get; set; }
        public AreaRestriction DomainRestriction { get; set; }
        public List<string> ResourceTypeRestriction { get; set; }
        public bool Trace { get; set; }

        private Uri _startUri;
        private readonly List<Uri> _retrievedUrls;

        public Crawler(int maxLayer, AreaRestriction domainRestriction, List<string> resourceTypeRestriction, bool trace)
        {
            MaxLayer = maxLayer;
            DomainRestriction = domainRestriction;
            ResourceTypeRestriction = resourceTypeRestriction;
            Trace = trace;
            _retrievedUrls = new List<Uri>();
        }

        public void GetSiteData(string startUrl, string destinationDirectory)
        {
            _retrievedUrls.Clear();

            _startUri = new Uri(startUrl);

            using var client = new HttpClient();

            if (Trace)
            {
                TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = "Downloading data is beginning." });
            }

            DownloadData(client, _startUri, destinationDirectory, 0);
        }

        private void DownloadData(HttpClient client, Uri startUri, string parentDirectory, int layer)
        {
            if (layer > MaxLayer || _retrievedUrls.Contains(startUri) || ResourceTypeRestriction.Any(rtr => startUri.Segments.Last().EndsWith(rtr)) || IsRestricted(startUri))
            {
                return;
            }

            if (Trace)
            {
                TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = $"found url: {startUri}" });
            }

            string currentDirectory = Path.Combine(parentDirectory, startUri.Host) + startUri.LocalPath.Replace("/", @"\");
            var response = client.GetAsync(startUri).Result;

            if (response.Content.Headers.ContentType?.MediaType == "text/html")
            {
                Directory.CreateDirectory(currentDirectory);

                string filePath = Path.Combine(currentDirectory, Path.GetFileName(currentDirectory) + ".html");

                var file = new FileStream(filePath, FileMode.Create);

                var document = new HtmlDocument();
                document.Load(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);
                document.Save(file);
                file.Close();
                _retrievedUrls.Add(startUri);

                var internalLinks = document.DocumentNode?.SelectNodes("//@href"/*|//@src"*/)?.Select(n => n.GetAttributeValue("href", n.GetAttributeValue("src", "default")));
                if (internalLinks.Any())
                {
                    foreach (var address in internalLinks)
                    {
                        if (Uri.TryCreate(address, UriKind.Absolute, out Uri link))
                        {
                            if (link.Scheme == "http" || link.Scheme == "https")
                            {
                                DownloadData(client, link, parentDirectory, layer + 1);
                            }
                        }
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(currentDirectory));
                var file = new FileStream(currentDirectory, FileMode.Create);
                response.Content.ReadAsStreamAsync().Result.CopyTo(file);
                file.Close();
                _retrievedUrls.Add(startUri);
            }
        }

        private bool IsRestricted(Uri startUri)
        {
            switch (DomainRestriction)
            {
                case AreaRestriction.Unlimited:
                    return false;
                case AreaRestriction.CurrentDomainOnly:
                    return _startUri.DnsSafeHost != startUri.DnsSafeHost;
                case AreaRestriction.NotHigherSourcePath:
                    return !_startUri.IsBaseOf(startUri);
                default:
                    return true;
            }
        }
    }
}