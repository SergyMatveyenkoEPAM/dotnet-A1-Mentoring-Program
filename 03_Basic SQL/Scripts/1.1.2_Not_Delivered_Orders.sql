SELECT OrderID,
  CASE
    WHEN  ShippedDate IS NULL
    THEN 'Not Shipped'
    ELSE CAST(ShippedDate AS NVARCHAR(30))
  END AS ShippedDate
  FROM [dbo].[Orders]   
  WHERE ShippedDate IS NULL