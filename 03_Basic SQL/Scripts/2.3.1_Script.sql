SELECT DISTINCT e.EmployeeID, e.FirstName, e.LastName
FROM [dbo].[Employees] e INNER JOIN [dbo].[EmployeeTerritories] et ON e.EmployeeID=et.EmployeeID
						INNER JOIN [dbo].[Territories] t ON et.TerritoryID=t.TerritoryID
						INNER JOIN [dbo].[Region] r ON t.RegionID=r.RegionID
WHERE r.RegionDescription = 'Western'