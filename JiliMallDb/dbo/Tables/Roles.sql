CREATE TABLE [dbo].[Roles]
(
  [Id] BIGINT IDENTITY (1, 1) NOT NULL,
  [RoleId] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
  [RoleName] NVARCHAR(20) NOT NULL,
  [RoleDescription] NVARCHAR(200) NOT NULL,
  [CreatedBy] BIGINT NULL,
  [CreatedDTM] DATETIME NULL,
  [UpdatedBy] BIGINT NULL,
  [UpdatedDTM] BIGINT NULL,
  [RowVersion] ROWVERSION,
  CONSTRAINT PK_Roles_Id PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [FK_Projects_Roles_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users]  ([ID]),
  CONSTRAINT [FK_Projects_Roles_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users]  ([ID]),

);
GO

CREATE NONCLUSTERED INDEX IX_Roles_RoleId
    ON [dbo].[Roles] ([RoleId])
GO

CREATE NONCLUSTERED INDEX UQ_Roles_RoleName
    ON [dbo].[Roles] ([RoleName])
GO

CREATE NONCLUSTERED INDEX UQ_Roles_RoleDescription
    ON [dbo].[Roles] ([RoleDescription])
GO