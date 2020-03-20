IF OBJECT_ID(N'[Northwind].[Regions]') IS NULL
BEGIN
	EXEC sp_rename 'Northwind.Region', 'Regions'
END
GO

IF NOT EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME='Northwind.Customers' AND COLUMN_NAME='EstablishmentDate')
BEGIN
	ALTER TABLE [Northwind].[Customers]	
		ADD [EstablishmentDate] DATETIME NULL	
END