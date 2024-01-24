CREATE TABLE [dbo].[UserRole] (
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]  BIT              CONSTRAINT [DF_UserRole_IsDeleted] DEFAULT ((0)) NOT NULL,
    [Created]    DATETIME2 (7)    NOT NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NOT NULL,
    [Modified]   DATETIME2 (7)    NOT NULL,
    [ModifiedBy] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


GO

CREATE NONCLUSTERED INDEX [IX_UserRole_IsDeleted]
    ON [dbo].[UserRole]([IsDeleted]);