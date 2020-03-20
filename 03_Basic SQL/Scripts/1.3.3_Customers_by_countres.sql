SELECT CustomerID, Country 
FROM [dbo].[Customers]
WHERE Country LIKE '[b-g]%' 
ORDER BY Country