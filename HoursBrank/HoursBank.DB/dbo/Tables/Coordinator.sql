﻿CREATE TABLE [dbo].[Coordinator]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Team_Id] INT NOT NULL, 
    [User_Id] INT NOT NULL,
    CONSTRAINT [FK_COORDINATOR_TEAM] FOREIGN KEY ([Team_Id]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [FK_COORDINATOR_USER] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
)