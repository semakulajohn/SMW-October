CREATE PROCEDURE [dbo].[Property_UnPublish]
	@DraftId	BIGINT
AS
	--Check if published property exists
	DECLARE @PublishedId	int = 0;

	SET @PublishedId	 = (
		select TOP 1 PropertyId from Property where  PropertyId  = @DraftId and Published = 1
	)

	BEGIN TRY
		  BEGIN TRANSACTION TRA_UnPublish

			IF(@PublishedId != 0 AND @PublishedId IS NOT NULL)
			BEGIN

				--Remove published date on draft record to be displayed in Admin
				Update Property Set PublishedOn = null,
				PublishedBy = null,
				UnPublishedOn= GETDATE(),
				Published = 0 where PropertyId = @DraftId

				

			END 
	COMMIT TRANSACTION TRA_UnPublish

		PRINT 'Operation Successful.'
END TRY

	BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UnPublish
				PRINT 'Error detected, all changes reversed'
		END
	END CATCH
