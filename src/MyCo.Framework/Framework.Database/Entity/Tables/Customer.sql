CREATE TABLE [Entity].[Customer] (
    [CustomerID]			INT IDENTITY (1, 1) CONSTRAINT [DF_Customer_CustomerID] NOT NULL,
	[CustomerKey]			UniqueIdentifier	CONSTRAINT [DF_Customer_CustomerKey] DEFAULT (NewID()) NOT NULL,
	[CustomerTypeID]		INT				CONSTRAINT [DF_Customer_CustomerType] DEFAULT (-1) NOT NULL,
    [FirstName]				NVARCHAR (50)   CONSTRAINT [DF_Customer_FirstName] DEFAULT ('') NOT NULL,
    [MiddleName]			NVARCHAR (50)   CONSTRAINT [DF_Customer_MiddleName] DEFAULT ('') NOT NULL,
    [LastName]				NVARCHAR (50)   CONSTRAINT [DF_Customer_LastName] DEFAULT ('') NOT NULL,
    [BirthDate]				DATETIME        CONSTRAINT [DF_Customer_BirthDate] DEFAULT ('01-01-1900') NOT NULL,
	[GenderID]				INT				CONSTRAINT [DF_Customer_GenderISO] DEFAULT ('-1') NOT NULL,
    [CreatedActivityID]		INT				CONSTRAINT [DF_Customer_CreatedActivity] DEFAULT(-1) NOT NULL,
    [ModifiedActivityID]	INT				CONSTRAINT [DF_Customer_ModifiedActivity] DEFAULT(-1) NOT NULL,
	[CreatedDate]			DATETIME        CONSTRAINT [DF_Customer_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedDate]			DATETIME        CONSTRAINT [DF_Customer_ModifiedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
	CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY ([CustomerTypeID]) REFERENCES [Entity].[CustomerType] ([CustomerTypeID]),
	CONSTRAINT [FK_Customer_CreatedActivity] FOREIGN KEY ([CreatedActivityID]) REFERENCES [Activity].[Activity] ([ActivityID]),
	CONSTRAINT [FK_Customer_ModifiedActivity] FOREIGN KEY ([ModifiedActivityID]) REFERENCES [Activity].[Activity] ([ActivityID]),
	CONSTRAINT [CC_Customer_Gender] CHECK ([GenderID] BETWEEN -1 AND 9)
);
GO
CREATE NonCLUSTERED INDEX [IX_Customer_All] ON [Entity].[Customer] ([FirstName] Asc, [MiddleName] Asc, [LastName] Asc, [BirthDate] Asc)
GO
CREATE UNIQUE NonCLUSTERED INDEX [IX_Customer_Key] ON [Entity].[Customer] ([CustomerKey] Asc)
GO
