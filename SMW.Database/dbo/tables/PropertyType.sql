CREATE TABLE [dbo].[PropertyType]
(
	[PropertyTypeId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Name]		NVARCHAR(MAX) NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    
	

	 CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED ([PropertyTypeId] ASC),

	
)
GO

