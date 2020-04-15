using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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

        public Crawler(int maxLayer, AreaRestriction domainRestriction, List<string> resourceTypeRestriction, bool trace)
        {
            MaxLayer = maxLayer;
            DomainRestriction = domainRestriction;
            ResourceTypeRestriction = resourceTypeRestriction;
            Trace = trace;
        }





        public void GetSiteData(string startUrl, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);
            string path = Path.Combine(destinationDirectory, (string.IsNullOrEmpty(Path.GetFileName(startUrl)) ? Path.GetFileName(Path.GetDirectoryName(startUrl)) : Path.GetFileName(startUrl)) + ".html");
            var file = new FileStream(path, FileMode.Create);






            using var client = new HttpClient();

            client.GetAsync(startUrl).Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

            TraceMessage?.Invoke(this, new OutputMessageEventArgs { Message = "message from lib: everything is fine" });
        }

    }
}
