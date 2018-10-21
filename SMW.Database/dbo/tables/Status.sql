CREATE TABLE [dbo].[Status]
(	
	[StatusId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Name]		NVARCHAR(50) NOT NULL,
	[CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,

	

	 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([StatusId] ASC),

)
GO

