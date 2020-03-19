SELECT DISTINCT (e.LastName + ' ' + e.FirstName) Employee,(e1.LastName + ' ' + e1.FirstName) Boss          
FROM [dbo].[Employees] e INNER JOIN [dbo].[Employees] e1 ON e.[ReportsTo]=e1.[EmployeeID]
ORDER BY 2, 1