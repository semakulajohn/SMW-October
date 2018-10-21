CREATE TABLE [dbo].[Facility]
(
[FacilityId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Description]		NVARCHAR(MAX) NOT NULL,
	[Dimensions]		NVARCHAR(MAX) NOT NULL,
	[Location]			NVARCHAR(MAX) NOT NULL,
	[FacilityTypeId]     BIGINT NOT NULL,
	[RentalPeriodId]    BIGINT NULL,
	[MediaFolderId]  BIGINT NOT NULL,	
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Facility] PRIMARY KEY CLUSTERED ([FacilityId] ASC),
	CONSTRAINT [FK_dbo_Facility_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Facility_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Facility_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Facility_MediaId] FOREIGN KEY  ([MediaFolderId]) REFERENCES [dbo].[Media](MediaId),
	CONSTRAINT [FK_dbo_Facility_RentalPeriodId] FOREIGN KEY  ([RentalPeriodId]) REFERENCES [dbo].[RentalPeriod](RentalPeriodId),
	CONSTRAINT [FK_dbo_Facility_FacilityTypeId] FOREIGN KEY  ([FacilityTypeId]) REFERENCES [dbo].[FacilityType](FacilityTypeId)
	
)
GO
ALTER TABLE [dbo].[Facility] ADD  CONSTRAINT [DF_dbo_Facility_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
