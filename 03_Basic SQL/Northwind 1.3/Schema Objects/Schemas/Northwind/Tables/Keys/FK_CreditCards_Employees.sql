ALTER TABLE [Northwind].[CreditCards]
    ADD CONSTRAINT [FK_CreditCards_Employees] FOREIGN KEY (EmployeeID) REFERENCES [Northwind].[Employees] ([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

