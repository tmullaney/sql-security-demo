-- Enable RLS
-- 
-- Add UserId column to schema 
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'UserId' AND Object_ID = Object_ID(N'Contacts'))
	ALTER TABLE Contacts ADD UserId nvarchar(128)
		DEFAULT CAST(SESSION_CONTEXT(N'UserId') AS nvarchar(128))
go

-- Assign existing data to existing users for demo
DECLARE @user1_id nvarchar(128), @user2_id nvarchar(128);
SELECT @user1_id = Id FROM AspNetUsers WHERE UserName = 'user1@contoso.com';
SELECT @user2_id = Id FROM AspNetUsers WHERE UserName = 'user2@contoso.com';
UPDATE Contacts SET UserId = @user1_id WHERE ContactId % 2 = 0;
UPDATE Contacts SET UserId = @user2_id WHERE ContactId % 2 = 1;
go

CREATE SCHEMA Security
go

CREATE FUNCTION Security.userAccessPredicate(@UserId nvarchar(128))
    RETURNS TABLE
    WITH SCHEMABINDING
AS
    RETURN SELECT 1 AS accessResult
    WHERE @UserId = CAST(SESSION_CONTEXT(N'UserId') AS nvarchar(128))
go

CREATE SECURITY POLICY Security.userSecurityPolicy
    ADD FILTER PREDICATE Security.userAccessPredicate(UserId) ON dbo.Contacts,
    ADD BLOCK PREDICATE Security.userAccessPredicate(UserId) ON dbo.Contacts
go

