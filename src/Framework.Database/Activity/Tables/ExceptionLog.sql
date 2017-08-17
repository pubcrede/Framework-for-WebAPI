CREATE TABLE [Activity].[ExceptionLog] (
    [ExceptionLogID]        INT            IDENTITY (1, 1) NOT NULL,
	[AssemblyName]			NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_AssemblyName] DEFAULT ('') NOT NULL,
	[Message]				NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_Message] DEFAULT ('') NOT NULL,
    [InnerException]		NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_InnerException] DEFAULT ('') NOT NULL,
    [StackTrace]			NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_StackTrace] DEFAULT ('') NOT NULL,
    [ADDomainName]			NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_ExceptionADDomainName] DEFAULT ('') NOT NULL,
    [ADUserName]			NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_ExceptionADUserName] DEFAULT ('') NOT NULL,
    [DirectoryWorking]		NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_ExceptionDirectoryWorking] DEFAULT ('') NOT NULL,
    [DirectoryAssembly]		NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_ExceptionDirectoryAssembly] DEFAULT ('') NOT NULL,    
    [URL]					NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_URL] DEFAULT ('') NOT NULL,
    [CustomMessage]			NVARCHAR (MAX)    CONSTRAINT [DF_ExceptionLog_CustomMessage] DEFAULT ('') NOT NULL,
	[Discriminator]			NVARCHAR (128)	 CONSTRAINT [DF_ExceptionLog_Discriminator] DEFAULT ('') NOT NULL,
	[CreatedActivityID]     INT				 CONSTRAINT [DF_ExceptionLog_CreatedActivityID] DEFAULT (-1) NOT NULL,
	[CreatedDate]           DATETIME         CONSTRAINT [DF_ExceptionLog_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Exception] PRIMARY KEY CLUSTERED ([ExceptionLogID] ASC)
	);
GO