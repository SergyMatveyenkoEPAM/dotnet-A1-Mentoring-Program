using ClosedXML.Excel;
using NorthwindApp.BLL.Interfaces;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.DAL.Models;
using NorthwindApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService()
        {
            _repository = new OrderRepository();
        }

        public XLWorkbook GetOrdersReport(string customerId, DateTime? dateFrom, DateTime? dateTo, int? take, int? skip)
        {
            var orders = _repository.GetAll().AsEnumerable();

            if (orders == null || !orders.Any())
            {
                return null;
            }

            orders = string.IsNullOrEmpty(customerId) ? orders : orders.Where(o => o.CustomerID == customerId);

            if (dateFrom != null)
            {
                orders = orders.Where(o => o.OrderDate >= dateFrom);
            }
            else if (dateTo != null)
            {
                orders = orders.Where(o => o.OrderDate <= dateTo);
            }

            orders = take == null ? orders : orders.Take((int)take);

            orders = skip == null ? orders : orders.Skip((int)skip);

            XLWorkbook workbook = CreateExcelFile(orders.OrderBy(o => o.OrderID));

            return workbook;
        }

        private XLWorkbook CreateExcelFile(IEnumerable<Order> orders)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Report");
            worksheet.Cell(1, 1).Value = nameof(Order.OrderID);
            worksheet.Cell(1, 2).Value = nameof(Order.CustomerID);
            worksheet.Cell(1, 3).Value = nameof(Order.EmployeeID);
            worksheet.Cell(1, 4).Value = nameof(Order.OrderDate);
            worksheet.Cell(1, 5).Value = nameof(Order.RequiredDate);
            worksheet.Cell(1, 6).Value = nameof(Order.ShippedDate);
            worksheet.Cell(1, 7).Value = nameof(Order.ShipVia);
            worksheet.Cell(1, 8).Value = nameof(Order.Freight);
            worksheet.Cell(1, 9).Value = nameof(Order.ShipName);
            worksheet.Cell(1, 10).Value = nameof(Order.ShipAddress);
            worksheet.Cell(1, 11).Value = nameof(Order.ShipCity);
            worksheet.Cell(1, 12).Value = nameof(Order.ShipRegion);
            worksheet.Cell(1, 13).Value = nameof(Order.ShipPostalCode);
            worksheet.Cell(1, 14).Value = nameof(Order.ShipCountry);

            int i = 1;
            foreach (var order in orders)
            {
                ++i;
                worksheet.Cell(i, 1).Value = order.OrderID;
                worksheet.Cell(i, 2).Value = order.CustomerID;
                worksheet.Cell(i, 3).Value = order.EmployeeID;
                worksheet.Cell(i, 4).Value = order.OrderDate;
                worksheet.Cell(i, 5).Value = order.RequiredDate;
                worksheet.Cell(i, 6).Value = order.ShippedDate;
                worksheet.Cell(i, 7).Value = order.ShipVia;
                worksheet.Cell(i, 8).Value = order.Freight;
                worksheet.Cell(i, 9).Value = order.ShipName;
                worksheet.Cell(i, 10).Value = order.ShipAddress;
                worksheet.Cell(i, 11).Value = order.ShipCity;
                worksheet.Cell(i, 12).Value = order.ShipRegion;
                worksheet.Cell(i, 13).Value = order.ShipPostalCode;
                worksheet.Cell(i, 14).Value = order.ShipCountry;
            }

            return workbook;
        }
    }
}
