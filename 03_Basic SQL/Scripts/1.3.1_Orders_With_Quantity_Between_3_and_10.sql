SELECT DISTINCT o.[OrderID]
FROM [dbo].[Orders] o INNER JOIN [dbo].[Order Details] od ON o.OrderID=od.OrderID
WHERE od.Quantity BETWEEN 3 AND 10