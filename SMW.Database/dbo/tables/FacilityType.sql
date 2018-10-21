CREATE TABLE [dbo].[FacilityType]
(
	[FacilityTypeId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Name]		NVARCHAR(MAX) NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    
	

	 CONSTRAINT [PK_FacilityType] PRIMARY KEY CLUSTERED ([FacilityTypeId] ASC),

	
)
GO


