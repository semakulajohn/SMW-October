CREATE TABLE [dbo].[Project]
(
	[ProjectId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Description]		NVARCHAR(MAX) NOT NULL,
	[Title]		NVARCHAR(MAX) NOT NULL,
	[ClientId]		NVARCHAR(128) NOT NULL,
	[MediaFolderId]  BIGINT NOT NULL,	
    [CreatedOn]		DATETIME NOT NULL, 
	[StatusId]     BIGINT NOT NULL,
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectId] ASC),
	CONSTRAINT [FK_dbo_Project_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Project_StatusId]  FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status](StatusId),
	CONSTRAINT [FK_dbo_Project_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Project_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Project_MediaId] FOREIGN KEY  ([MediaFolderId]) REFERENCES [dbo].[Media](MediaId)
	
)
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_dbo_Project_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

