SELECT CompanyName, Country
  FROM [dbo].[Customers]
  WHERE (Country NOT IN ('USA', 'Canada')) OR Country IS NULL
  ORDER BY CompanyName