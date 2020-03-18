SELECT OrderID, ShippedDate, ShipVia 
  FROM [dbo].[Orders]
  WHERE ShippedDate>=convert(datetime,'05.06.1998', 121) AND ShipVia>=2