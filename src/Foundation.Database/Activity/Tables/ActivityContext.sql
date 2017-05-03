CREATE TABLE [Activity].[ActivityContext] (
	[ActivityContextID]		INT IDENTITY (1, 1) CONSTRAINT [DF_ActivityContext_ActivityContextID] NOT NULL,
	[DeviceUUID]			NVARCHAR (250)   CONSTRAINT [DF_ActivityContext_Device] DEFAULT ('') NOT NULL,	
	[ApplicationUUID]		NVARCHAR (250)   CONSTRAINT [DF_ActivityContext_Application] DEFAULT ('') NOT NULL,	
	[IdentityUserName]		NVARCHAR (40)    CONSTRAINT [DF_ActivityContext_IdentityUserName] DEFAULT ('') NOT NULL,	    
	[PrincipalIP4Address]	NVARCHAR (15)    CONSTRAINT [DF_ActivityContext_PrincipalIP4Address] DEFAULT ('') NOT NULL,
	[StackTrace]			NVARCHAR (4000)  CONSTRAINT [DF_ActivityContext_StackTrace] DEFAULT ('') NOT NULL,
	[ActivitySQLStatement]	NVARCHAR (2000)  CONSTRAINT [DF_ActivityContext_ActivitySQLStatement] DEFAULT ('') NOT NULL,    
	[ActivityHierarchy]		hierarchyid		 CONSTRAINT [DF_ActivityContext_ActivityHierarchy] DEFAULT (hierarchyid::GetRoot()) NULL,
	[ExecutingContext]		NVARCHAR (2000)  CONSTRAINT [DF_ActivityContext_ExecutingContext] DEFAULT ('') NOT NULL,    
    [CreatedDate]			DATETIME         CONSTRAINT [DF_ActivityContext_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedDate]			DATETIME         CONSTRAINT [DF_ActivityContext_ModifiedDate] DEFAULT (getutcdate()) NOT NULL,	
	[Discriminator]			NVARCHAR (128)	 CONSTRAINT [DF_ActivityContext_Discriminator] DEFAULT ('') NOT NULL,
	CONSTRAINT [PK_ActivityContext] PRIMARY KEY CLUSTERED ([ActivityContextID] ASC)
);
GO
