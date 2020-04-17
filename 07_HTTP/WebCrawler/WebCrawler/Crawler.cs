using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;

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
        private readonly List<string> _retrievedUrls;

        public Crawler(int maxLayer, AreaRestriction domainRestriction, List<string> resourceTypeRestriction, bool trace)
        {
            MaxLayer = maxLayer;
            DomainRestriction = domainRestriction;
            ResourceTypeRestriction = resourceTypeRestriction;
            Trace = trace;
            _retrievedUrls = new List<string>();
        }

        public void GetSiteData(string startUrl, string destinationDirectory)
        {
            //Directory.CreateDirectory(destinationDirectory);

            _retrievedUrls.Clear();

            using var client = new HttpClient();

            if (Trace)
            {
                TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = "Downloading data is beginning." });
            }

            DownloadData(client, startUrl, destinationDirectory, 0);
            //client.GetAsync(startUrl).Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

        }

        private void DownloadData(HttpClient client, string startUrl, string parentDirectory, int layer)
        {
            if (layer > MaxLayer)
            {
                return;
            }

            if (Trace)
            {
                TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = $"found url: {startUrl}" });
            }

            //string s = startUrl.Split("//")[1];
            //string s1 = s.Replace("/", @"\");
            //var array1 = s1.Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray();
            //var array= startUrl.Split("//")[1].Replace("/", @"\").Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray();

            string currentDirectory = Path.Combine(parentDirectory, new string(startUrl.Split("//")[1].Replace("/", @"\").Where(ch => !Path.GetInvalidFileNameChars().Contains(ch)).ToArray()));
            Directory.CreateDirectory(currentDirectory);

            string path = Path.Combine(currentDirectory, Path.GetFileName(currentDirectory) + ".html");

            var file = new FileStream(path, FileMode.Create);

            var response = client.GetAsync(startUrl).Result;
            var document = new HtmlDocument();
            document.Load(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8);
            // MemoryStream memoryStream = new MemoryStream();
            document.Save(file);
            file.Close();
            _retrievedUrls.Add(startUrl);

            if (response.Content.Headers.ContentType?.MediaType == "text/html")
            {
                var internalLinks = document.DocumentNode.SelectNodes("//@href"/*|//@src"*/).Select(n => n.GetAttributeValue("href", n.GetAttributeValue("src", "default")));
                foreach (var link in internalLinks)
                {
                    if (Trace)
                    {
                        // TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = link });
                        // HtmlAttribute att = link.Attributes["src"];
                        // TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = link.GetAttributeValue("src","нету") });
                    }
                    DownloadData(client, link, parentDirectory, layer + 1);
                }
            }



            var internalLinks1 = document.DocumentNode.SelectNodes("//@href");
            var internalLinks2 = document.DocumentNode.SelectNodes("//@src");

        }
    }
}