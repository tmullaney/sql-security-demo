using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Microsoft.AspNet.Identity;

// unnecessary
using System.Data.Sql;

namespace ContactManager.Models
{
    public class SessionContextDbConnectionInterceptor : IDbConnectionInterceptor
    {
        public void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            //System.Diagnostics.Debug.WriteLine(String.Format("Opened. State: {0}", connection.State.ToString())); // todo: remove diagnostics printing when done debugging
            try
            {
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                if (userId != null)
                {
                    //DbCommand getContext = connection.CreateCommand();
                    //getContext.CommandText = "SELECT SESSION_CONTEXT(N'UserId')";
                    //using (DbDataReader reader = getContext.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        System.Diagnostics.Debug.WriteLine(String.Format("SESSION_CONTEXT: {0}", reader[0]));
                    //    }
                    //}

                    DbCommand cmd = connection.CreateCommand();
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = "@UserId";
                    param.Value = userId;
                    cmd.CommandText = "execute sp_set_session_context @key=N'UserId', @value=@UserId"; // todo: make read_only when sp_reset_connection bug is fixed
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    //System.Diagnostics.Debug.WriteLine(String.Format("Set context: {0} (param: {1})", cmd.CommandText, param.Value.ToString()));
                }
            }
            catch (System.NullReferenceException)
            {
                // If no user is logged in, leave SESSION_CONTEXT null (all rows will be filtered)
            }
        }

        public void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            // No operation
        }

        public void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            // No operation
        }

        public void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            // No operation
            //System.Diagnostics.Debug.WriteLine(String.Format("Closed. State: {0}", connection.State.ToString()));
        }

        public void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            // No operation
            //System.Diagnostics.Debug.WriteLine(String.Format("Closing. State: {0}", connection.State.ToString()));
        }

        public void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {
            // No operation
        }

        public void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {
            // No operation
        }

        public void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            // No operation
            //System.Diagnostics.Debug.WriteLine("Disposed");
        }

        public void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            // No operation
            //System.Diagnostics.Debug.WriteLine("Disposing");
        }

        public void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {
            // No operation
        }

        public void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {
            // No operation
        }

        public void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            // No operation
            //System.Diagnostics.Debug.WriteLine(String.Format("Opening. State: {0}", connection.State.ToString()));
        }

        public void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            // No operation
        }

        public void StateGetting(DbConnection connection, DbConnectionInterceptionContext<System.Data.ConnectionState> interceptionContext)
        {
            // No operation
        }

        public void StateGot(DbConnection connection, DbConnectionInterceptionContext<System.Data.ConnectionState> interceptionContext)
        {
            // No operation
        }
    }

    public class SessionContextConfiguration : DbConfiguration
    {
        public SessionContextConfiguration()
        {
            AddInterceptor(new SessionContextDbConnectionInterceptor());
        }
    }
}