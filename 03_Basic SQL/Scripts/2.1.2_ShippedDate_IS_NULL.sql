SELECT COUNT(CASE 
				WHEN ShippedDate IS NULL 
				THEN 1 
			 END)
FROM [dbo].[Orders]