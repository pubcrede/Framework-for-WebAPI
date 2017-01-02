CREATE TABLE [Activity].[ExceptionLog] (
    [ExceptionLogID]        INT            IDENTITY (1, 1) NOT NULL,
	[AssemblyName]			NVARCHAR (MAX)    CONSTRAINT [DF_Exception_AssemblyName] DEFAULT ('') NOT NULL,
	[Message]				NVARCHAR (MAX)    CONSTRAINT [DF_Exception_Message] DEFAULT ('') NOT NULL,
    [InnerException]		NVARCHAR (MAX)    CONSTRAINT [DF_Exception_InnerException] DEFAULT ('') NOT NULL,
    [StackTrace]			NVARCHAR (MAX)    CONSTRAINT [DF_Exception_StackTrace] DEFAULT ('') NOT NULL,
    [ADDomainName]			NVARCHAR (MAX)    CONSTRAINT [DF_Exception_ExceptionADDomainName] DEFAULT ('') NOT NULL,
    [ADUserName]			NVARCHAR (MAX)    CONSTRAINT [DF_Exception_ExceptionADUserName] DEFAULT ('') NOT NULL,
    [DirectoryWorking]		NVARCHAR (MAX)    CONSTRAINT [DF_Exception_ExceptionDirectoryWorking] DEFAULT ('') NOT NULL,
    [DirectoryAssembly]		NVARCHAR (MAX)    CONSTRAINT [DF_Exception_ExceptionDirectoryAssembly] DEFAULT ('') NOT NULL,    
    [URL]					NVARCHAR (MAX)    CONSTRAINT [DF_Exception_URL] DEFAULT ('') NOT NULL,
    [CustomMessage]			NVARCHAR (MAX)    CONSTRAINT [DF_Exception_CustomMessage] DEFAULT ('') NOT NULL,
	[Discriminator]			NVARCHAR (128)	 CONSTRAINT [DF_Exception_Discriminator] DEFAULT ('') NOT NULL,
	[CreatedActivityID]     INT				 CONSTRAINT [DF_Exception_CreatedActivityID] DEFAULT (-1) NOT NULL,
	[CreatedDate]           DATETIME         CONSTRAINT [DF_Exception_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Exception] PRIMARY KEY CLUSTERED ([ExceptionLogID] ASC));
GO