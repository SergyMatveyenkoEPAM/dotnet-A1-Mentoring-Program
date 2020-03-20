SELECT e.LastName, e.FirstName
FROM [dbo].[Employees] e 
WHERE (SELECT COUNT(o.OrderID) FROM [dbo].[Orders] o WHERE e.EmployeeID=o.EmployeeID) > 150
