namespace ContactManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ContactManager.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.IO;
    using System.Text.RegularExpressions;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(ContactManager.Models.ApplicationDbContext context)
        {
            // Add two sample users (via Identity Framework)
            //var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //um.Create(new ApplicationUser() { UserName = "user1@contoso.com" }, "user1password");
            //um.Create(new ApplicationUser() { UserName = "user2@contoso.com" }, "user2password");

            // Run included SQL scripts to seed sample data and enable security features
            //var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //this.executeSqlFile(context, baseDir + "\\sql\\01-Seed-Contacts.sql");
            //this.executeSqlFile(context, baseDir + "\\sql\\02-Enable-Row-Level-Security.sql");
        }

        private void executeSqlFile(ContactManager.Models.ApplicationDbContext context, string sqlFile)
        {
            // Split "GO"-sepatated batches into separate SqlCommands
            var sqlScriptText = File.ReadAllText(sqlFile);
            var batchTexts = Regex.Split(sqlScriptText, "\r\nGO\r\n", RegexOptions.IgnoreCase);
            foreach (var batchText in batchTexts)
            {
                try
                {
                    context.Database.ExecuteSqlCommand(batchText);
                }
                catch (System.ArgumentException)
                {
                    // Ignore empty commands
                }
            }
        }
    }
}
