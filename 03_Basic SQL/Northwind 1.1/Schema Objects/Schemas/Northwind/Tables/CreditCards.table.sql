CREATE TABLE [Northwind].[CreditCards](
	[CreditCardId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Number] INT NOT NULL,
	[ExpiryDate] DATETIME NOT NULL,
	[CardHolder] NVARCHAR(50) NOT NULL,
	[EmployeeID] INT NOT NULL	
)
