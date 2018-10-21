CREATE PROCEDURE [dbo].[Mark_Comment_And_RelatedData_AsDeleted]
	
	@inPutCommentId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateCommentRelatedDetails

			
		
	 Update [Comment]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE CommentId =@inPutCommentId AND Deleted = 0
	 
	 
 COMMIT TRANSACTION TRA_UpdateCommentRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateCommentRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH