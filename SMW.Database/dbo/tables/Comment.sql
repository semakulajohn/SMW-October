CREATE TABLE [dbo].[Comment]
(
	[CommentId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Body]		NVARCHAR(MAX) NOT NULL,
	[ParentId]	BIGINT NULL, 
    [CreatedOn]		DATETIME NOT NULL, 
	[MediaId]		BIGINT NOT NULL,
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentId] ASC),
	 CONSTRAINT [FK_Comment_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Comment]([CommentId]),
	CONSTRAINT [FK_dbo_Comment_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Comment_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Comment_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Comment_MediaId] FOREIGN KEY  ([MediaId]) REFERENCES [dbo].[Media](MediaId)
	
)
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_dbo_Comment_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
