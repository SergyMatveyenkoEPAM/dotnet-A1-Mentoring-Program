SELECT s.CompanyName
FROM [dbo].[Suppliers] s
WHERE s.SupplierID IN (SELECT p.SupplierID FROM [dbo].[Products] p WHERE p.UnitsInStock=0)