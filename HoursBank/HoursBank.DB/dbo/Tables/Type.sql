CREATE TABLE [dbo].[Type]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Description] VARCHAR(50) NOT NULL, 
    [Increase] BIT NOT NULL
)
