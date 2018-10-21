CREATE PROCEDURE [dbo].[Mark_Service_And_RelatedData_AsDeleted]
	
	@inPutServiceId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateServiceRelatedDetails

			
		
	 Update [Service]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE ServiceId =@inPutServiceId AND Deleted = 0
	 
	 
 COMMIT TRANSACTION TRA_UpdateServiceRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateServiceRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH


