CREATE TABLE [dbo].[User]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Email] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(50) NOT NULL,
    [ClientId] VARCHAR(50) NULL,
    [ClientSecret] VARCHAR(50) NULL,
    [Hours] FLOAT NOT NULL,
    [Admin] BIT NULL DEFAULT 0,
    [Active] BIT NULL DEFAULT 0,
    [TeamId] INT NOT NULL,
    CONSTRAINT [FK_USER_TEAM] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
)
