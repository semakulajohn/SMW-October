CREATE TABLE [dbo].[Video]
(
	[VideoId] [BIGINT] IDENTITY(1,1) NOT NULL,
	[VideoGuid]	UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
	[VersionGuid]	UNIQUEIDENTIFIER NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[VideoUrl] [varchar](max) NOT NULL,
	[ProjectId] [BIGINT] NULL,		
	[ClientId] NVARCHAR(128) NOT NULL,
	[CreatedOn] [datetime] NULL, 
	[TimeStamp] [datetime] NULL,
	[Deleted]   BIT NOT NULL,	
	[DeletedOn] DATETIME NULL,
	[CreatedBy] NVARCHAR(128) NULL, 
    [UpdatedBy] NVARCHAR(128) NULL, 
    [DeletedBy] NVARCHAR(128) NULL,	 
	[UnPublishedBy]   VARCHAR(128) NULL,
	[PublishedOn] DATETIME NULL,
	[Published] BIT NOT NULL,
	[PublishedBy] NVARCHAR(128) NULL,
	[ScheduledPublishOn] DATETIME NULL,
	[UnPublishOn] DATETIME NULL,
	[ScheduledUnPublishOn] DATETIME NULL,

	CONSTRAINT [Fk_dbo_Video_PublishedBy] FOREIGN KEY ([PublishedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_Video_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_Video_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([ProjectId]),
	CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED ([VideoId] ASC)) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Video] ADD CONSTRAINT [FK_dbo_Video_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id)
GO
ALTER TABLE [dbo].[Video] ADD CONSTRAINT [FK_dbo_Video_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id)
GO
ALTER TABLE [dbo].[Video] ADD	CONSTRAINT [FK_dbo_Video_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id)
GO

