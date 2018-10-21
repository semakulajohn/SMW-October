CREATE TABLE [dbo].[WebQuery]
(
	[WebQueryId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Body]		NVARCHAR(MAX) NOT NULL,
	[PhoneNumber]		NVARCHAR(MAX) NOT NULL,
	[Name]		NVARCHAR(50) NOT NULL,
	[EmailAddress]  NVARCHAR(50) NOT NULL,	
	[CreatedOn]		DATETIME NOT NULL, 
	[RespondedTo]   BIT NOT NULL,
    	

	 CONSTRAINT [PK_WebQuery] PRIMARY KEY CLUSTERED ([WebQueryId] ASC),
	
)


