Create View [EntityCode].[CustomerType]
As
Select	CT.[CustomerTypeID] As [ID],
		CT.[CustomerTypeKey] As [Key],
		CT.[CustomerTypeName] As [Name], 
		CT.[CreatedDate], 
		CT.[ModifiedDate]
From	[Entity].[CustomerType] CT
