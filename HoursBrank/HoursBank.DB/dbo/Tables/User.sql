CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Client_Id] VARCHAR(50) NULL, 
    [Client_Secret] VARCHAR(50) NULL, 
    [Hours] FLOAT NOT NULL, 
    [Admin] BIT NULL DEFAULT 0, 
    [Active] BIT NULL DEFAULT 0, 
    [Team_Id] INT NOT NULL,
    CONSTRAINT [FK_USER_TEAM] FOREIGN KEY ([Team_Id]) REFERENCES [dbo].[Team] ([Id])
)
