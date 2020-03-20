SELECT c.[CompanyName], e.LastName + ' ' + e.FirstName, c.[City]          
FROM [dbo].[Customers] c, [dbo].[Employees] e 
WHERE e.[City] = c.[City]
