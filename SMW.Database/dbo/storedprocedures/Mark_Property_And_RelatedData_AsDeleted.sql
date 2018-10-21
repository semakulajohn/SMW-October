CREATE PROCEDURE [dbo].[Mark_Property_And_Related_DataAs_Deleted]
	@inPutPropertyId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdatePropertyRelatedDetails

			
		
	 Update [Property]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE PropertyId =@inPutPropertyId AND Deleted = 0
	 
	 
 COMMIT TRANSACTION TRA_UpdatePropertyRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdatePropertyRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH

