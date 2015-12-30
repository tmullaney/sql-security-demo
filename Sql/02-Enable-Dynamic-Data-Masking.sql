-- Enable Dynamic Data Masking
-- appUser does not have UNMASK permission, so columns will appear masked to application
-- administrator does have UNMASK permission, so columns will appear unmasked for them
ALTER TABLE Contacts ALTER COLUMN Email ADD MASKED WITH (FUNCTION = 'email()')
ALTER TABLE Contacts ALTER COLUMN Address ADD MASKED WITH (FUNCTION = 'default()')

