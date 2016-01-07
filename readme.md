# Azure SQL Database Security Demo - ContactManager Web App

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

Click to deploy a live web app on Azure that shows how you can use SQL Database's new security features to protect your data. This demo is a simple contact manager application built using the ASP.NET MVC framework.

### To run the demo:
1. Click 'Deploy to Azure' (sign in with your Azure account if necessary)
2. Create passwords for the application login and server admin login accounts used to connect to the database
3. Launch the deployment (this may take several minutes to complete)
4. Browse to the site URL to start using the web app
   1. Try logging in as user1@contoso.com (password: user1password), or as user2@contoso.com (password: user2password), to see what the app looks like to end users.

### What gets created:
* Web Hosting Plan (Free tier)
* Web App (ASP.NET MVC with Entity Framework)
* SQL Server and Database (Basic tier, about $0.01 per hour)
  * Two SQL users: appLogin and adminLogin
  * Two app users: user1@contoso.com and user2@contoso.com
  * A handful of sample contacts
* Storage Account 

### Security features used in the demo:
* [Row-Level Security](https://msdn.microsoft.com/library/dn765131.aspx) prevents users from seeing contacts that do not belong to them. 
* [Dynamic Data Masking](https://azure.microsoft.com/documentation/articles/sql-database-dynamic-data-masking-get-started/) masks the street address and email fields, so that users must request access to this information from an administrator.
* [Transparent Data Encryption](https://msdn.microsoft.com/library/dn948096.aspx) encrypts all data at rest.

