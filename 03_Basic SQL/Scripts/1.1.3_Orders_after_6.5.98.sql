SELECT OrderID AS [Order Number], 
  CASE
    WHEN  ShippedDate IS NULL
    THEN 'Not Shipped'
    ELSE CAST(ShippedDate AS NVARCHAR(30))
  END AS [Shipped Date] 
  FROM [dbo].[Orders]
  WHERE ShippedDate>=convert(datetime,'05.06.1998', 121) OR ShippedDate IS NULL