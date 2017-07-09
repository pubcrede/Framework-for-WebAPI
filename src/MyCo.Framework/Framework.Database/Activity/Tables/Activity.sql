CREATE TABLE [Activity].[Activity] (
	[ActivityID]			INT IDENTITY (1, 1) CONSTRAINT [DF_Activity_ActivityID] NOT NULL,
	[DeviceUUID]			NVARCHAR (250)   CONSTRAINT [DF_Activity_Device] DEFAULT ('') NOT NULL,	
	[ApplicationUUID]		NVARCHAR (250)   CONSTRAINT [DF_Activity_Application] DEFAULT ('') NOT NULL,	
	[IdentityUserName]		NVARCHAR (40)    CONSTRAINT [DF_Activity_IdentityUserName] DEFAULT ('') NOT NULL,	    
	[PrincipalIP4Address]	NVARCHAR (15)    CONSTRAINT [DF_Activity_PrincipalIP4Address] DEFAULT ('') NOT NULL,
	[StackTrace]			NVARCHAR (4000)  CONSTRAINT [DF_Activity_StackTrace] DEFAULT ('') NOT NULL,
	[ActivitySQLStatement]	NVARCHAR (2000)  CONSTRAINT [DF_Activity_ActivitySQLStatement] DEFAULT ('') NOT NULL,    
	[ActivityHierarchy]		hierarchyid		 CONSTRAINT [DF_Activity_ActivityHierarchy] DEFAULT (hierarchyid::GetRoot()) NULL,
	[ExecutingContext]		NVARCHAR (2000)  CONSTRAINT [DF_Activity_ExecutingContext] DEFAULT ('') NOT NULL,    
    [CreatedDate]			DATETIME         CONSTRAINT [DF_Activity_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedDate]			DATETIME         CONSTRAINT [DF_Activity_ModifiedDate] DEFAULT (getutcdate()) NOT NULL,	
	[Discriminator]			NVARCHAR (128)	 CONSTRAINT [DF_Activity_Discriminator] DEFAULT ('') NOT NULL,
	CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
);
GO
