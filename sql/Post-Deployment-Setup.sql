-- Post-Deployment Data Setup
--
-- Add UserId column to associate each contact with a UserId
ALTER TABLE Contacts ADD UserId nvarchar(128)
    --DEFAULT CAST(SESSION_CONTEXT(N'UserId') AS nvarchar(128))
go

-- Assign existing sample data to sample users
DECLARE @user1_id nvarchar(128), @user2_id nvarchar(128)
SELECT @user1_id = Id FROM AspNetUsers WHERE UserName = 'user1@contoso.com'
SELECT @user2_id = Id FROM AspNetUsers WHERE UserName = 'user2@contoso.com'
UPDATE Contacts SET UserId = @user1_id WHERE ContactId % 2 = 0
UPDATE Contacts SET UserId = @user2_id WHERE ContactId % 2 = 1
go

