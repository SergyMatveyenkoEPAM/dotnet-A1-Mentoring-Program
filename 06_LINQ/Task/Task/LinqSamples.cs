// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Home work")]
        [Title("Task 1")]
        [Description("The list of customers with a turnover more than X")]
        public void Linq001()
        {
            List<decimal> list = new List<decimal> { 10000m, 20000m, 30000m };

            list.ForEach(x => GetCustomers(x));

            void GetCustomers(decimal amount)
            {
                var customers = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > amount);

                ObjectDumper.Write($"amount = {amount}");

                foreach (var customer in customers)
                {
                    ObjectDumper.Write(customer.Orders.Sum(o => o.Total) + " " + customer.CompanyName);
                }

                ObjectDumper.Write("---------------------------");
            }
        }

        [Category("Home work")]
        [Title("Task 2 with grouping")]
        [Description("The list of suppliers in the same country and the same city as a customer")]
        public void Linq002()
        {
            var result = dataSource.Customers
                .GroupJoin(dataSource.Suppliers,
                    c => new { c.City, c.Country },
                    s => new { s.City, s.Country },
                    (cust, supp) => new
                    {
                        Key = cust,
                        Items = supp
                    })
                .OrderByDescending(r => r.Items.Any())
                .ThenBy(r => r.Key.Country);

            foreach (var element in result)
            {
                ObjectDumper.Write(element.Key.Country + " " + element.Key.City + " " + element.Key.CompanyName);

                foreach (var supplier in element.Items)
                {
                    ObjectDumper.Write(supplier.Country + " " + supplier.City + " " + supplier.SupplierName);
                }

                ObjectDumper.Write("---------------------------");
            }
        }

        [Category("Home work")]
        [Title("Task 2 without grouping")]
        [Description("The list of suppliers in the same country and the same city as a customer")]
        public void Linq002_1()
        {
            var result = dataSource.Customers
                .Select(c => new
                {
                    Key = c,
                    Items = dataSource.Suppliers.Where(s => s.Country == c.Country && s.City == c.City)
                })
                .OrderByDescending(r => r.Items.Any())
                .ThenBy(r => r.Key.Country);

            foreach (var element in result)
            {
                ObjectDumper.Write(element.Key.Country + " " + element.Key.City + " " + element.Key.CompanyName);

                foreach (var supplier in element.Items)
                {
                    ObjectDumper.Write(supplier.Country + " " + supplier.City + " " + supplier.SupplierName);
                }

                ObjectDumper.Write("---------------------------");
            }
        }

        [Category("Home work")]
        [Title("Task 3")]
        [Description("The list of customers who an order with a price bigger than X")]
        public void Linq003()
        {
            decimal boundaryPrice = 5000m;
            var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > boundaryPrice));

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CompanyName + " " + customer.Orders?.FirstOrDefault(o => o.Total > boundaryPrice)?.Total);
            }
        }

        [Category("Home work")]
        [Title("Task 4")]
        [Description("The list of customers with the date of starting cooperation")]
        public void Linq004()
        {
            var customers = dataSource.Customers
                .Select(customer => new
                {
                    FirstOrderDate = customer.Orders.FirstOrDefault(order =>
                        customer.Orders.Any() && order.OrderDate == customer.Orders.Min(o => o.OrderDate))?.OrderDate,
                    CompanyName = customer.CompanyName
                });

            foreach (var customer in customers)
            {
                ObjectDumper.Write((customer.FirstOrderDate?.ToString("yyyy-MM ") ?? "has no orders ") + customer.CompanyName);
            }
        }

        [Category("Home work")]
        [Title("Task 5")]
        [Description("Sorted list of customers with the date of starting cooperation")]
        public void Linq005()
        {
            var customers = dataSource.Customers
                .Select(customer => new
                {
                    FirstOrderDate = customer.Orders.FirstOrDefault(order => customer.Orders.Any() && order.OrderDate == customer.Orders.Min(o => o.OrderDate))?.OrderDate,
                    CompanyName = customer.CompanyName
                })
                .OrderByDescending(c => c.FirstOrderDate).ThenBy(c => c.CompanyName);

            foreach (var customer in customers)
            {
                ObjectDumper.Write((customer.FirstOrderDate?.ToString("yyyy-MM ") ?? "has no orders ") + customer.CompanyName);
            }
        }

        [Category("Home work")]
        [Title("Task 6")]
        [Description("The list of customers without a digital postcode or region is not specified or telephone number does not contain an operator code")]
        public void Linq006()
        {
            int postalCode;

            var customers = dataSource.Customers
                    .Where(customer => int.TryParse(customer.PostalCode, out postalCode)
                                       || string.IsNullOrEmpty(customer.Region)
                                       || Regex.IsMatch(customer.Phone.TrimStart(), "^[(]"));

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CompanyName + " " + customer.PostalCode + " " + customer.Phone + " " + (string.IsNullOrEmpty(customer.Region) ? "has no region" : customer.Region));
            }
        }

        [Category("Home work")]
        [Title("Task 7")]
        [Description("Grouped products by category then by the number of units in stock")]
        public void Linq007()
        {
            var collection = from product in dataSource.Products
                             group product by product.Category into categoryGroup
                             select new
                             {
                                 Category = categoryGroup.Key,
                                 UnitsInStockGroup = from product in categoryGroup
                                                     group product by product.UnitsInStock into unitsInStockGroup
                                                     select new
                                                     {
                                                         NumberOfUnits = unitsInStockGroup.Key,
                                                         Products = unitsInStockGroup.OrderBy(p => p.UnitPrice)
                                                     }
                             };

            foreach (var element in collection)
            {
                ObjectDumper.Write("Category - " + element.Category);
                foreach (var unitsInStock in element.UnitsInStockGroup)
                {
                    ObjectDumper.Write("Number of units - " + unitsInStock.NumberOfUnits);
                    foreach (var product in unitsInStock.Products)
                    {
                        ObjectDumper.Write(product.Category + " " + product.UnitsInStock + " " + product.UnitPrice + " " + product.ProductName);
                    }
                }
            }
        }

        [Category("Home work")]
        [Title("Task 8")]
        [Description("The list of products grouped by price")]
        public void Linq008()
        {
            decimal firstBar = 20m;
            decimal secondBar = 50m;

            var collection = dataSource.Products
                .GroupBy(p => p.UnitPrice < firstBar ? "Cheap"
                    : p.UnitPrice < secondBar ? "Average"
                    : "Expensive");

            foreach (var products in collection)
            {
                ObjectDumper.Write(products.Key);
                foreach (var product in products)
                {
                    ObjectDumper.Write(product.UnitPrice + " " + product.ProductName);
                }
            }
        }

        [Category("Home work")]
        [Title("Task 9")]
        [Description("Average profit and average intensity of each city")]
        public void Linq009()
        {
            var collection = dataSource.Customers.GroupBy(c => c.City, (city, customers) => new
            {
                City = city,
                AverageProfit = customers.SelectMany(customer => customer.Orders.Select(o => o.Total)).Average(),
                AverageIntensity = customers.Average(customer => customer.Orders.Count())
            });

            foreach (var item in collection)
            {
                ObjectDumper.Write(item.City + " " + item.AverageProfit + " " + item.AverageIntensity);
            }
        }

        [Category("Home work")]
        [Title("Task 10")]
        [Description("Average annual intensity and average month and average month of the year")]
        public void Linq010()
        {
            var customers = dataSource.Customers.Select(customer => new
            {

                Name = customer.CompanyName,
                AverageAnnualIntensity = customer.Orders.GroupBy(order => order.OrderDate.Year, (y, o) => new
                {
                    Year = y,
                    Intensity = o.Count()
                }),
                AverageMonthIntensity = customer.Orders.GroupBy(order => order.OrderDate.Month, (m, o) => new
                {
                    Month = m,
                    Intensity = o.Count()
                }),
                AverageMonthOfTheYearIntensity = customer.Orders.GroupBy(order => new { order.OrderDate.Year, order.OrderDate.Month }, (m, o) => new
                {
                    MonthOfTheYear = m,
                    Intensity = o.Count()
                })
            });

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.Name);
                foreach (var item in customer.AverageAnnualIntensity)
                {
                    ObjectDumper.Write(item.Year + " " + item.Intensity);
                }
                ObjectDumper.Write("------------------------");

                foreach (var item in customer.AverageMonthIntensity)
                {
                    ObjectDumper.Write(item.Month + " " + item.Intensity);
                }
                ObjectDumper.Write("------------------------");

                foreach (var item in customer.AverageMonthOfTheYearIntensity)
                {
                    ObjectDumper.Write(item.MonthOfTheYear.Year + " " + item.MonthOfTheYear.Month + " " + item.Intensity);
                }
                ObjectDumper.Write("************************");
            }
        }
    }
}