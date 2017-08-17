Create Procedure [EntityCode].[CustomerInfoDelete]
	@ID	INT,
	@ActivityContextID		INT
AS
	Delete
	From	[Entity].[Customer]
	Where	CustomerID = @ID