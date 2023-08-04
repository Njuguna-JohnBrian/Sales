CREATE TABLE [dbo].[Users]
(
    [Id]                  BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]              UNIQUEIDENTIFIER       NOT NULL DEFAULT (NEWID()),
    [FirstName]           NVARCHAR(50)           NOT NULL,
    [LastName]            NVARCHAR(50)           NOT NULL,
    [Email]               NVARCHAR(50)           NOT NULL,
    [PasswordHash]        NVARCHAR(200)          NOT NULL,
    [ResetToken]          NVARCHAR(1000)         NULL,
    [ResetTokenExpiryDTM] DATETIME               NULL,
    [RegistrationDTM]     DATETIME               NOT NULL,
    [UpdatedDTM]          DATETIME               NULL,
    [IsDeleted]           BIT                    NOT NULL DEFAULT (0),
    [DeletedBy]           BIGINT                 NULL,
    [DeletedDTM]          DATETIME               NULL,
    [RowVersion]          ROWVERSION,
    CONSTRAINT PK_Users_Id PRIMARY KEY CLUSTERED ([Id])

);
GO

CREATE NONCLUSTERED INDEX IX_Users_UserId
    ON [dbo].[Users] ([UserId])
GO

CREATE NONCLUSTERED INDEX UQ_Users_Email
    ON [dbo].[Users] ([Email])
GO