CREATE TABLE [dbo].[RolePermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [PermissionKey] INT NOT NULL, 
    [PermissionValue] BIT NOT NULL, 
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
)
