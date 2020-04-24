using ClosedXML.Excel;
using System;
using System.IO;

namespace NorthwindApp.BLL.Interfaces
{
    public interface IOrderService
    {
        XLWorkbook GetOrdersReport(string customerId, DateTime? dateFrom, DateTime? dateTo, int? take, int? skip);

        MemoryStream GetXmlOrdersReport(string customerId, DateTime? dateFrom, DateTime? dateTo, int? take, int? skip);
    }
}
