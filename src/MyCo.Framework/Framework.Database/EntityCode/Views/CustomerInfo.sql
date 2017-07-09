Create View [EntityCode].[CustomerInfo]
As
Select	C.[CustomerID] As [ID],
		C.[CustomerKey] As [Key],
		C.[FirstName], 
		C.[MiddleName], 
		C.[LastName], 
		C.[BirthDate], 
		C.[GenderID],
		CT.[CustomerTypeKey],
		C.[CreatedActivityID] As [ActivityID],
		C.[CreatedDate], 
		C.[ModifiedDate]
From	[Entity].[Customer] C
Join	[Entity].[CustomerType] CT On C.CustomerTypeID = CT.CustomerTypeID
