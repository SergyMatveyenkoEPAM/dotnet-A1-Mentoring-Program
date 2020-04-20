using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindApp.BLL.Interfaces;
using NorthwindApp.BLL.Services;

namespace NorthwindApp
{
    public class OrderReportHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.Url.PathAndQuery == "/~close")
                return;

            var name = request.QueryString["name"];

            var answerString = string.IsNullOrEmpty(name) ?
                "Hello, Anonymouse!" : $"Hello, {name}";

            // NorthwindDB _db=new NorthwindDB();
            IOrderService service = new OrderService();

            var res = service.GetAll().Take(10).Select(o => o.ShipName);

            foreach (var r in res)
            {
                response.Output.WriteLine(r+"<br/>");
            }
            response.Output.WriteLine(answerString);
        }
    }
}