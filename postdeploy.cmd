ECHO Executing postdeploy.cmd
DIR

ECHO Running sql scripts...
SET _conn=%SQLAZURECONNSTR_DefaultConnection%
SET _conn=%_conn: =%                                        &:: remove whitespace
SET _conn="%_conn:;=";"%"                                   &:: add quotes around each piece
FOR %%a IN (%_conn%) DO @FOR /F %%b IN (%%a) DO SET _%%b    &:: set pieces as variables
SET _conn=

sqlcmd -S %_DataSource% -d %_InitialCatalog% -U %_UserId% -P %_Password% -i .\Sql\00-Initial-Migrations.sql

ECHO Finished executing postdeploy.cmd
