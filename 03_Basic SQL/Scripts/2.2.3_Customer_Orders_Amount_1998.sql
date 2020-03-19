SELECT  COUNT(OrderID) AS Amount
FROM [dbo].[Orders]
WHERE DATEPART(year, OrderDate) = 1998
GROUP BY EmployeeID, CustomerID
