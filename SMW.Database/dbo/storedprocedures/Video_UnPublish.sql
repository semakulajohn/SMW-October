CREATE PROCEDURE [dbo].[Video_UnPublish]
	@DraftId BIGINT
AS
	--Check if published video exists
	DECLARE @PublishedId	BIGINT = 0;

	SET @PublishedId	 = (
		select TOP 1 VideoId from Video where VersionGuid in (select VersionGuid from Video where VideoId  = @DraftId) and VideoId  != @DraftId and Published = 1
	)

	BEGIN TRY
		  BEGIN TRANSACTION TRA_UnPublish

			IF(@PublishedId != 0 AND @PublishedId IS NOT NULL)
			BEGIN

				--Remove published date on draft record to be displayed in Admin
				Update Video Set PublishedOn = null where VideoId = @DraftId

				DELETE Video where VideoId = @PublishedId

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
