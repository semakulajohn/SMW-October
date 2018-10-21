CREATE TABLE [dbo].[Service]
(
	[ServiceId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Description]		NVARCHAR(MAX) NOT NULL,
	[Title]		NVARCHAR(MAX) NOT NULL,
	[MediaFolderId]  BIGINT NOT NULL,	
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([ServiceId] ASC),
	CONSTRAINT [FK_dbo_Service_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Service_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Service_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Service_MediaId] FOREIGN KEY  ([MediaFolderId]) REFERENCES [dbo].[Media](MediaId)
	
)
GO
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_dbo_Service_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

