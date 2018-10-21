CREATE PROCEDURE [dbo].[Video_Publish]
	@VideoId BIGINT, 
	@PublishedBy NVARCHAR (128)
AS	
	DECLARE @PublishedVideoId	int = 0

	IF(NOT EXISTS(
		select TOP 1 VideoId from Video where VersionGuid in (select VersionGuid from video where videoId  = @VideoId) and videoId  != @VideoId and Published = 1
		))

		BEGIN
																				
			INSERT INTO Video
				(Timestamp, Title, Description,ProjectId, VideoUrl, Published, PublishedOn, PublishedBy, VersionGuid, CreatedOn, Deleted,ClientId,CreatedBy)
			SELECT 
				GETDATE(), Title, Description, ProjectId,VideoUrl, 1, PublishedOn, @PublishedBy ,VersionGuid, CreatedOn, 0,ClientId,CreatedBy
			FROM video
			WHERE VideoId = @VideoId

			SET @PublishedVideoId = (SELECT @@IDENTITY)

		END
	ELSE --Update Video
		BEGIN
			SET @PublishedVideoId	 = (
				select TOP 1 VideoId from Video where VersionGuid in (select VersionGuid from video where videoId  = @VideoId) and videoId  != @VideoId and Published = 1
			)

			Update Video SET
				
				Video.Title = tV.Title,
				Video.Description = tV.Description,
				video.ProjectId = tV.ProjectId,
				Video.VideoUrl = tV.VideoUrl,					
				Video.PublishedOn = GETDATE(),
				Video.PublishedBy = @PublishedBy,
				Video.[Timestamp] = GETDATE(),
				Video.Deleted = 0,
				Video.ClientId=tV.ClientId,
				Video.CreatedBy = tV.CreatedBy
			from Video
			INNER JOIN
				Video tV
			ON
				Video.VersionGuid = tV.VersionGuid
			WHERE
				tV.VideoId= @VideoId
				AND Video.VideoId != @VideoId
		END


	--Set published date on draft record to be displayed in Admin
	Update Video Set PublishedOn = GetDaTe(), PublishedBy = @PublishedBy where VideoId = @VideoId

	--return new DocumentId
	SELECT @PublishedVideoId AS VideoId


