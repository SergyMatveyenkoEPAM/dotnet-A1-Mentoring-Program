using System;
using System.Collections.Generic;
using System.IO;
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
            IOrderService service = new OrderService();

            string customerId = request.QueryString["customerId"];
            DateTime? dateFrom = Convert.ToDateTime(request.QueryString["dateFrom"]);
            DateTime? dateTo = Convert.ToDateTime(request.QueryString["dateTo"]);
            int? take = Convert.ToInt32(request.QueryString["take"]);
            int? skip = Convert.ToInt32(request.QueryString["skip"]);


            
            var workbook = service.GetOrdersReport(null, null, null, 10, null);
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(response.OutputStream);
                memoryStream.Close();
            }

            response.End();
        }
    }
}