CREATE TABLE [dbo].[User] (
    [Id]                       UNIQUEIDENTIFIER NOT NULL,
    [FirstName]                NVARCHAR (100)   NULL,
    [LastName]                 NVARCHAR (100)   NULL,
    [Email]                    NVARCHAR (250)   NOT NULL,
    [NormalizedEmail]          NVARCHAR (250)   NOT NULL,
    [PreferredLanguageISOCode] NVARCHAR (10)    NOT NULL,
    [IsStaff]                  BIT              NOT NULL,
    [IsDeleted]                BIT              CONSTRAINT [DF_User_IsDeleted] DEFAULT ((0)) NOT NULL,
    [Created]                  DATETIME2 (7)    NOT NULL,
    [CreatedBy]                UNIQUEIDENTIFIER NOT NULL,
    [Modified]                 DATETIME2 (7)    NOT NULL,
    [ModifiedBy]               UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_User_NormalizedEmail]
    ON [dbo].[User]([NormalizedEmail]);

GO
CREATE NONCLUSTERED INDEX [IX_User_IsDeleted]
    ON [dbo].[User]([IsDeleted]);
