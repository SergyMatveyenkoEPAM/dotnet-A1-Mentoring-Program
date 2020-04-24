using NorthwindApp.BLL.Interfaces;
using NorthwindApp.BLL.Services;
using System;
using System.IO;
using System.Web;

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
            DateTime? dateFrom = string.IsNullOrEmpty(request.QueryString["dateFrom"]) ? (DateTime?)null : Convert.ToDateTime(request.QueryString["dateFrom"]);
            DateTime? dateTo = string.IsNullOrEmpty(request.QueryString["dateTo"]) ? (DateTime?)null : Convert.ToDateTime(request.QueryString["dateTo"]);
            int? take = string.IsNullOrEmpty(request.QueryString["take"]) ? (int?)null : Convert.ToInt32(request.QueryString["take"]);
            int? skip = string.IsNullOrEmpty(request.QueryString["skip"]) ? (int?)null : Convert.ToInt32(request.QueryString["skip"]);

            if (request.Headers["Accept"] == "text/xml" || request.Headers["Accept"] == "application/xml")
            {
                var ordersStream = service.GetXmlOrdersReport(customerId, dateFrom, dateTo, take, skip);

                ordersStream.WriteTo(response.OutputStream);

                response.ContentType = "text/xml";

                response.Flush();
                response.End();
            }
            else
            {
                var workbook = service.GetOrdersReport(customerId, dateFrom, dateTo, take, skip);
                if (workbook != null)
                {
                    response.Clear();
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.AddHeader("content-disposition", "attachment;filename=\"OrdersReport.xlsx\"");

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
    }
}