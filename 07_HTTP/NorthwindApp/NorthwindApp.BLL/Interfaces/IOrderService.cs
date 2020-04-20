using ClosedXML.Excel;
using System;

namespace NorthwindApp.BLL.Interfaces
{
    public interface IOrderService
    {
        XLWorkbook GetOrdersReport(string customerId, DateTime? dateFrom, DateTime? dateTo, int? take, int? skip);
    }
}
