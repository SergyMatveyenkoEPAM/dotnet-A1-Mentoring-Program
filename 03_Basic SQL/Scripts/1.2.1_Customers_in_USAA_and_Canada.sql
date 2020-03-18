SELECT CompanyName, Country
  FROM [dbo].[Customers]
  WHERE Country IN ('USA', 'Canada')
  ORDER BY CompanyName, Country