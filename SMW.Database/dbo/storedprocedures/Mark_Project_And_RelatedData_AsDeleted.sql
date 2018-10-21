CREATE PROCEDURE [dbo].[Mark_Project_And_RelatedData_AsDeleted]
	
	@inPutProjectId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateProjectRelatedDetails

			
		
	 Update [Project]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE ProjectId =@inPutProjectId AND Deleted = 0
	 
	 
 COMMIT TRANSACTION TRA_UpdateProjectRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateProjectRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH


