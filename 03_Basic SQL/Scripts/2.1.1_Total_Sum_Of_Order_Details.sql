SELECT SUM ([UnitPrice] * [Quantity] * (1-[Discount])) AS Total
FROM [dbo].[Order Details]