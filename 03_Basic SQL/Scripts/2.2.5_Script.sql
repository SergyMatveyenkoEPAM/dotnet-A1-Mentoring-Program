SELECT DISTINCT c.[CompanyName], c.[City]          
FROM [dbo].[Customers] c INNER JOIN [dbo].[Customers] c1 ON c.City=c1.City
WHERE c.[CustomerID]<>c1.[CustomerID]
ORDER BY 2,1
