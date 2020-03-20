SELECT CustomerID, Country 
FROM [dbo].[Customers]
WHERE Country BETWEEN 'b' AND 'h'
ORDER BY Country