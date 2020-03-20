SELECT DATEPART(year, OrderDate) AS [Year], COUNT(OrderID) AS Total
FROM [dbo].[Orders]
GROUP BY DATEPART(year, OrderDate)

SELECT COUNT(*) AS [Total Orders count]
FROM [dbo].[Orders]