CREATE TABLE [dbo].[RentalPeriod]
(
[RentalPeriodId] 	BIGINT IDENTITY(1,1) NOT NULL,		 
	[Name]		NVARCHAR(MAX) NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    
	

	 CONSTRAINT [PK_RentalPeriod] PRIMARY KEY CLUSTERED ([RentalPeriodId] ASC),

	
)
GO


