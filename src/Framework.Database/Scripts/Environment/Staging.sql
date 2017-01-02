PRINT N'Environment-specific script: Staging.sql';

--------------------------------------------------------------
-- Customer Test
--------------------------------------------------------------
-- Activity required for any insert, update and delete
Declare @ActivityID_Staging As Int
Insert INTO [Activity].[Activity] ([IdentityUserName]) Select N'SQLScript@Genesys.com' As IdentityUserName
Select	@ActivityID_Staging = SCOPE_IDENTITY()

MERGE INTO [Entity].[Customer] AS Target 
USING (VALUES 
	(N'9A530E38-6B22-48CA-95FB-182D5A64C754', N'36B08B23-0C1D-4488-B557-69665FD666E1', N'John', N'M', N'Smith', N'05/20/1968'),
	(N'87761D52-BB75-4A10-9178-675A89782667', N'BF3797EE-06A5-47F2-9016-369BEB21D944', N'Siva', N'N', N'Kumar', N'11/15/1976'),
	(N'ABC80489-53B3-4F6D-BDC2-135E569885C5', N'51A84CE1-4846-4A71-971A-CB610EEB4848', N'Maki', N'L', N'Ishii', N'06/30/1973')
	)
AS Source ([CustomerKey], [CustomerTypeKey], [FirstName], [MiddleName], [LastName], [BirthDate])
	Join [Entity].[CustomerType] CT On Source.CustomerTypeKey = CT.CustomerTypeKey
ON Target.[CustomerKey] = Source.[CustomerKey]
-- Update
WHEN MATCHED THEN 
	UPDATE SET [FirstName] = Source.[FirstName], [MiddleName] = Source.[MiddleName], [CustomerTypeID] = CT.[CustomerTypeID],
		[LastName] = Source.[LastName], [BirthDate] = Source.[BirthDate], [ModifiedActivityID] = @ActivityID_Staging
-- Insert 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([CustomerKey], [FirstName], [MiddleName], [LastName], [BirthDate], [CustomerTypeID], [CreatedActivityID], [ModifiedActivityID]) 
		Values (Source.[CustomerKey], Source.[FirstName], Source.[MiddleName], Source.[LastName], Source.[BirthDate], CT.[CustomerTypeID], @ActivityID_Staging, @ActivityID_Staging)
; -- SHARED table, do not delete records. Allow other sources (i.e. DBA, other apps, etc.) to insert records
--WHEN NOT MATCHED BY SOURCE THEN 
--	DELETE;
