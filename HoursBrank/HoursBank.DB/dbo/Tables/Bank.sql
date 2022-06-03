CREATE TABLE [dbo].[Bank]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Start] DATETIME NULL, 
    [End] DATETIME NULL, 
    [Description] VARCHAR(150) NULL, 
    [Approved] BIT NULL, 
    [Date_Approved] DATETIME NULL, 
    [Created_At] DATETIME NULL, 
    [User_Id] INT NULL, 
    [Type_Id] INT NULL,
    CONSTRAINT [FK_BANK_USER] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_BANK_TYPE] FOREIGN KEY ([Type_Id]) REFERENCES [dbo].[Type] ([Id])
)
