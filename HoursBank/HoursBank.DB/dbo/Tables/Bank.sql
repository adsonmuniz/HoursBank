CREATE TABLE [dbo].[Bank]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Start] DATETIME NULL,
    [End] DATETIME NULL,
    [Description] VARCHAR(150) NULL,
    [Approved] BIT NULL,
    [DateApproved] DATETIME NULL,
    [CreatedAt] DATETIME NULL,
    [UserId] INT NULL,
    [TypeId] INT NULL,
    CONSTRAINT [FK_BANK_USER] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_BANK_TYPE] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[Type] ([Id])
)
