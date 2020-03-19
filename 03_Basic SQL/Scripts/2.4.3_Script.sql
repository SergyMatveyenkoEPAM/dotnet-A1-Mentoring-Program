SELECT c.CompanyName
FROM [dbo].[Customers] c 
WHERE NOT EXISTS (SELECT * FROM [dbo].[Orders] o WHERE c.CustomerID=o.CustomerID)