IF OBJECT_ID(N'[Northwind].[CreditCards]') IS NULL
BEGIN
	CREATE TABLE [Northwind].[CreditCards](
		[CreditCardId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[Number] INT NOT NULL,
		[ExpiryDate] DATETIME NOT NULL,
		[CardHolder] NVARCHAR(50) NOT NULL,
		[EmployeeID] INT NOT NULL	
	)
	ALTER TABLE [Northwind].[CreditCards]
    ADD CONSTRAINT [FK_CreditCards_Employees] FOREIGN KEY (EmployeeID) REFERENCES [Northwind].[Employees] ([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
END
GO