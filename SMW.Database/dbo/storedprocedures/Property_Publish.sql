CREATE PROCEDURE [dbo].[Property_Publish]
	@PropertyId BIGINT, 
	@PublishedBy NVARCHAR (128)
AS	
	DECLARE @PublishedPropertyId	bigint = 0

	IF(NOT EXISTS(
		select TOP 1 PropertyId from Property where VersionGuid in (select VersionGuid from Property where propertyId  = @PropertyId) and propertyId  != @PropertyId and Published = 1
		))

		BEGIN
																				
			INSERT INTO Property
				(
				Timestamp, MediaFolderId, Description,Location, PropertyFee, Published, PublishedOn, PublishedBy, VersionGuid, CreatedOn, Deleted,CreatedBy)
			SELECT 
				GETDATE(), MediaFolderId, Description, Location,PropertyFee, 1, PublishedOn, @PublishedBy ,VersionGuid, CreatedOn,0,CreatedBy
			FROM Property
			WHERE PropertyId = @PropertyId

			SET @PublishedPropertyId = (SELECT @@IDENTITY)

		END
	ELSE --Update Property
		BEGIN
			SET @PublishedPropertyId	 = (
				select TOP 1 PropertyId from Property where VersionGuid in (select VersionGuid from property where propertyId  = @PropertyId) and propertyId  != @PropertyId and Published = 1
			)

			Update Property SET
				
				Property.Location = tV.Location,
				Property.Description = tV.Description,
				Property.PropertyFee = tV.PropertyFee,	
				Property.PublishedOn = GETDATE(),
				Property.PublishedBy = @PublishedBy,
				Property.[Timestamp] = GETDATE(),
				Property.Deleted = 0,
				Property.MediaFolderId = tV.MediaFolderId,
				Property.CreatedBy = tV.CreatedBy
			from Property
			INNER JOIN
				Property tV
			ON
				Property.VersionGuid = tV.VersionGuid
			WHERE
				tV.PropertyId= @PropertyId
				AND Property.PropertyId != @PropertyId
		END


	--Set published date on draft record to be displayed in Admin
	Update Property Set PublishedOn = GetDaTe(), PublishedBy = @PublishedBy where PropertyId = @PropertyId

	--return new PropertyId
	SELECT @PublishedPropertyId AS PropertyId



