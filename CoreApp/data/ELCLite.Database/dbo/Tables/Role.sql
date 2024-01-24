CREATE TABLE [dbo].[Role] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (100)   NOT NULL,
    [NormalizedName] NVARCHAR (100)   NOT NULL,
    [Description]    NVARCHAR (250)   NULL,
    [IsDeleted]      BIT              CONSTRAINT [DF_Role_IsDeleted] DEFAULT ((0)) NOT NULL,
    [Created]        DATETIME2 (7)    NOT NULL,
    [CreatedBy]      UNIQUEIDENTIFIER NOT NULL,
    [Modified]       DATETIME2 (7)    NOT NULL,
    [ModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Role_NormalizedName]
    ON [dbo].[Role]([NormalizedName] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Role_IsDeleted]
    ON [dbo].[Role]([IsDeleted]);