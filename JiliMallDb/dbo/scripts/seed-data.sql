-- Write your own SQL object definition here, and it'll be included in your package.

-- User Table Seed Data

SET
    IDENTITY_INSERT [dbo].[Users] ON;
;
WITH usersSeed
    AS
    (SELECT *
     FROM (VALUES (1,
                   N'd4a98758-994e-47c0-8d3c-73b4738d3cdb',
                   N'Test',
                   N'Test',
                   N'test@email.com',
                   N'$2a$11$kBL9t.CA3e4I3kGum5GisOQ1aLhy3V.RpCs21MoYYT70fZb6reoqy',
                   GETDATE())) val([Id], [UserId], [FirstName], [LastName], [Email], [PasswordHash], [RegistrationDTM]))
    MERGE [dbo].[Users] AS Target
USING usersSeed AS Source
ON Source.ID = Target.ID
WHEN NOT MATCHED BY Target THEN
    INSERT
    ([
Id
]
        ,
     [
     UserId
     ]
        ,
     [
     FirstName
     ]
        ,
     [
     LastName
     ]
        ,
     [
     Email
     ]
        ,
     [
     PasswordHash
     ]
        ,
     [
     RegistrationDTM
     ])
    VALUES (Source.[Id], Source.[UserId], Source.[FirstName], Source.[LastName], Source.[Email], Source.[PasswordHash],
            Source.[RegistrationDTM])
    ;

SET
    IDENTITY_INSERT [dbo].[Users] OFF


-- Roles Table Seed Data

SET IDENTITY_INSERT [dbo].[Roles] ON;
;
WITH rolesSeed
    AS
    (SELECT *
     FROM (VALUES (1,
                   N'd4a98758-994e-47c0-8d3c-73b4738d3cdb',
                   N'Admin',
                   N'Has all access'),
                  (2,
                   N'd4a98758-995e-47c0-8d3c-73b4738d3cdb',
                   N'User',
                   N'Has normal access')) val([Id], [RoleId], [RoleName], [RoleDescription]))
    MERGE [dbo].[Roles] AS Target
USING rolesSeed AS Source
ON Source.ID = Target.ID
WHEN NOT MATCHED BY Target THEN
    INSERT
    ([
Id
]
        ,
     [
     RoleId
     ]
        ,
     [
     RoleName
     ]
        ,
     [
     RoleDescription
     ])
    VALUES (Source.[Id], Source.[RoleId], Source.[RoleName], Source.[RoleDescription])
    ;

SET
    IDENTITY_INSERT [dbo].[Roles] OFF