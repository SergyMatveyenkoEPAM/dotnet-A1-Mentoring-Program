using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NorthwindApp.Tests
{
    [TestFixture]
    class OrderReportHandlerTests
    {
        [TestCase("https://localhost:44356/")]
        [TestCase("https://localhost:44356?customer=VINET")]
        [TestCase("https://localhost:44356?dateFrom=1996-08-01")]
        [TestCase("https://localhost:44356?dateTo=1996-08-16")]
        [TestCase("https://localhost:44356?take=10")]
        [TestCase("https://localhost:44356?customer=VINET&take=10")]
        [TestCase("https://localhost:44356?customer=VINET&skip=10")]
        public async Task OrderReportHandler_Query_Xml(string url)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            await httpClient.GetAsync(url);
        }

        [TestCase("https://localhost:44356/")]
        [TestCase("https://localhost:44356?customer=VINET")]
        [TestCase("https://localhost:44356?dateFrom=1996-08-01")]
        [TestCase("https://localhost:44356?dateTo=1996-08-16")]
        [TestCase("https://localhost:44356?take=10")]
        [TestCase("https://localhost:44356?customer=VINET&take=10")]
        [TestCase("https://localhost:44356?customer=VINET&skip=10")]
        public async Task OrderReportHandler_Query_Excel(string url)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            await httpClient.GetAsync(url);
        }
    }
}
