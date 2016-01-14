ECHO Executing postdeploy.cmd

ECHO Running sql scripts...
SET _conn=%SQLAZURECONNSTR_DefaultConnection%
SET _conn=%_conn: =%                                        &:: remove whitespace
SET _conn="%_conn:;=";"%"                                   &:: add quotes around each piece
FOR %%a IN (%_conn%) DO @FOR /F %%b IN (%%a) DO SET _%%b    &:: set pieces as variables
SET _conn=

sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -Q "CREATE USER %APPSETTING_applicationLogin% WITH PASSWORD = '%APPSETTING_applicationLoginPassword%'"
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -Q "ALTER ROLE db_datareader ADD MEMBER %APPSETTING_applicationLogin%"
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -Q "ALTER ROLE db_datawriter ADD MEMBER %APPSETTING_applicationLogin%"
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -i .\Sql\00-Initial-Migrations.sql
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -i .\Sql\01-Enable-Row-Level-Security.sql
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -i .\Sql\02-Enable-Dynamic-Data-Masking.sql
sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %APPSETTING_administratorLogin% -P %APPSETTING_administratorLoginPassword% -i .\Sql\03-Enable-Transparent-Data-Encryption.sql

ECHO Finished executing postdeploy.cmd
