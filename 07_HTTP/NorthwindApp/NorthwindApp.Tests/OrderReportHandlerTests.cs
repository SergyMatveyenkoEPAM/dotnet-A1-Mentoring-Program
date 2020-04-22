using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NorthwindApp.Tests
{
    [TestFixture]
    class OrderReportHandlerTests
    {
        [Test]
        public async Task OrderReportHandler_Query_Xml()
        {
          //  string url = "https://localhost:44356/";
          //  var httpClient = new HttpClient();
          //  httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("text/xml"));
          //  MemoryStream memoryStream=new MemoryStream();
          //  FileStream file = File.Create(@"d:\qwer.xml");



          //  var response= httpClient.GetAsync(url).Result.;
          //  var header = response.Content;
          //  await httpClient.GetAsync(url).Result.Content.CopyToAsync(memoryStream);
          //  var streamWriter = new StreamWriter(memoryStream);
          //  streamWriter.AutoFlush = true;
          //memoryStream.Flush();
          //  Console.SetOut(streamWriter);


          //  // Console.WriteLine(memoryStream);
          //  Assert.AreEqual();
        }
    }
}
