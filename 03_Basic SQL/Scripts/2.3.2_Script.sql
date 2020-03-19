SELECT c.CompanyName, COUNT(o.OrderID)
FROM [dbo].[Customers] c LEFT JOIN [dbo].[Orders] o ON c.CustomerID=o.CustomerID
GROUP BY c.CompanyName
ORDER BY COUNT(o.OrderID)