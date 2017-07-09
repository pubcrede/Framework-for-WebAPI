Create Procedure [EntityCode].[CustomerInfoDelete]
	@ID	INT,
	@ActivityID		INT
AS
	Delete
	From	[Entity].[Customer]
	Where	CustomerID = @ID