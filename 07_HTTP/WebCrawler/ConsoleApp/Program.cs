using System;
using System.Collections.Generic;
using WebCrawler;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new Crawler(1, AreaRestriction.CurrentDomainOnly, new List<string> { ".ico", ".png" }, true);
            crawler.TraceMessage += (sender, e) => Console.WriteLine(e.Message);

            crawler.GetSiteData("https://stackoverflow.com/questions/56107/what-is-the-best-way-to-parse-html-in-c", @"D:\_Here we go");
            // crawler.GetSiteData("https://www.bbc.com/russian/", @"D:\_Here we go");
        }
    }
}