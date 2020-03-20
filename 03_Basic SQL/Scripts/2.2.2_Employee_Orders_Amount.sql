SELECT o.EmployeeID AS [Seller]
		,(SELECT e1.LastName + ' ' + e1.FirstName 
			FROM [dbo].[Employees] e1 
			WHERE e1.EmployeeID=o.EmployeeID) AS [Name]
		,Count(o.OrderID) AS Amount
FROM [dbo].[Orders] o INNER JOIN [dbo].[Employees] e ON o.EmployeeID=e.EmployeeID
GROUP BY o.EmployeeID
ORDER BY Amount
