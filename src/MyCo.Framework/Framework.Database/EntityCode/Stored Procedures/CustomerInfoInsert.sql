Create Procedure [EntityCode].[CustomerInfoInsert]
	@FirstName		nvarchar(50),
	@MiddleName		nvarchar(50),
	@LastName		nvarchar(50),
	@BirthDate		datetime,
	@GenderID		int,
	@CustomerTypeKey uniqueidentifier,
	@ActivityID		int
AS
	-- Local variables
	Declare @ID As int
	Declare @Key As uniqueidentifier
	Declare @EntityID As Int
	Declare @CustomerTypeID As int
	-- Initialize
	Select 	@FirstName		= RTRIM(LTRIM(@FirstName))
	Select 	@MiddleName		= RTRIM(LTRIM(@MiddleName))
	Select 	@LastName		= RTRIM(LTRIM(@LastName))

	-- Only update with good data
	If ((@FirstName <> '') Or (@MiddleName <> '') Or (@LastName <> '')) And @ActivityID <> -1
	Begin
		-- Insert
		Begin Try
			-- Get CustomerTypeID from key
			Select @CustomerTypeID = IsNull(CustomerTypeID, -1) From [Entity].[CustomerType] Where CustomerTypeKey = @CustomerTypeKey
			-- Create Customer record
			Select @Key = NewID()
			Insert Into [Entity].[Customer] (CustomerKey, FirstName, MiddleName, LastName, BirthDate, GenderID, CustomerTypeID, CreatedActivityID, ModifiedActivityID)
				Values (@Key, @FirstName, @MiddleName, @LastName, @BirthDate, @GenderID, @CustomerTypeID, @ActivityID, @ActivityID)	
			Select	@ID = SCOPE_IDENTITY()
		End Try
		Begin Catch
			Exec [Activity].[ExceptionLogInsert] @ActivityID
		End Catch
	End	

	-- Return data
	Select	IsNull(@ID, -1) As ID
