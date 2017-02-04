Create Procedure [EntityCode].[CustomerInfoUpdate]
	@ID				int,
	@FirstName		nvarchar(50),
	@MiddleName		nvarchar(50),
	@LastName		nvarchar(50),
	@BirthDate		datetime,
	@GenderID		int,
	@CustomerTypeKey uniqueidentifier,
	@ActivityID		int
AS
	-- Local variables
	Declare @Key As Uniqueidentifier
	Declare @CustomerTypeID As int
	-- Initialize
	Select 	@FirstName		= RTRIM(LTRIM(@FirstName))
	Select 	@MiddleName		= RTRIM(LTRIM(@MiddleName))
	Select 	@LastName		= RTRIM(LTRIM(@LastName))
	
	-- Get any missing data
	Select  @CustomerTypeID = IsNull(CustomerTypeID, -1) From [Entity].[CustomerType] Where CustomerTypeKey = @CustomerTypeKey
	-- Validate data that will be inserted/updated, and ensure basic values exist
	If ((@FirstName <> '') Or (@MiddleName <> '') Or (@LastName <> '')) And (@ActivityID <> -1) And (@CustomertypeID Is Not Null)
	Begin
		-- Insert vs. update
		Select @ID = C.[CustomerID] From [Entity].[Customer] C Where C.[CustomerID] = @ID
		-- Insert vs Update
		Begin Try
			If (@ID Is Null) Or (@ID = -1)
			Begin
				-- This was an insert passed to the update. Create Customer record
				Select @Key = NewID()
				Insert Into [Entity].[Customer] (CustomerKey, FirstName, MiddleName, LastName, BirthDate, GenderID, CustomerTypeID, CreatedActivityID, ModifiedActivityID)
					Values (@Key, @FirstName, @MiddleName, @LastName, @BirthDate, @GenderID, @CustomerTypeID, @ActivityID, @ActivityID)	
				Select	@ID = SCOPE_IDENTITY()
			End
			Else
			Begin
				-- Create main entity record
				Update P
				Set P.FirstName				= @FirstName, 
					P.MiddleName			= @MiddleName, 
					P.LastName				= @LastName, 
					P.BirthDate				= @BirthDate, 
					P.GenderID				= @GenderID,
					P.CustomerTypeID		= @CustomerTypeID,
					P.ModifiedActivityID	= @ActivityID,
					P.ModifiedDate			= GetUTCDate()
				From	[Entity].[Customer] P
				Where	P.CustomerID = @ID
			End
		End Try
		Begin Catch
			Exec [Activity].[ExceptionLogInsert] @ActivityID
		End Catch
	End	

	-- Return data
	Select	IsNull(@ID, -1) As ID
