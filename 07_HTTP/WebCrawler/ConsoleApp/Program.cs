using System;
using System.Threading.Channels;
using WebCrawler;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new Crawler(0,AreaRestriction.CurrentDomainOnly,null,true);
            crawler.TraceMessage+=(sender,e)=>Console.WriteLine(e.Message);

            crawler.GetSiteData("https://gomel.today/", @"D:\_Here we go");


        }
    }
}
