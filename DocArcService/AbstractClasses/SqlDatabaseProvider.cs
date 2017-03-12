using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using System.Configuration;

namespace DocArcService.AbstractClasses
{
    public abstract class SqlDatabaseProvider : IDatabaseProvider
    {
        private docarcdbEntities _ent;

        public docarcdbEntities Ent
        {
            get
            {
                return _ent;
            }
        }

        public SqlDatabaseProvider()
        {
            _ent = new docarcdbEntities();

            SetConnectionSettings();
        }

        public void SetConnectionSettings()
        {
            var formatString = Ent.Database.Connection.ConnectionString.ToString();

            var dbServer = ConfigurationManager.AppSettings["DbServer"];
            var dbName = ConfigurationManager.AppSettings["DbName"];
            var dbUser = ConfigurationManager.AppSettings["DbUser"];
            var dbPass = ConfigurationManager.AppSettings["DbPass"];


            var conString = string.Format(formatString, dbServer, dbName, dbUser, dbPass);

            Ent.Database.Connection.ConnectionString = conString;
        }

        public abstract Users GetUserByProviderUserName(string ProviderUserName);

        public abstract Users GetUserById(string UserId);

        public bool DatabaseIsReachable()
        {
            try
            {
                Ent.Database.Connection.Open();
                return Ent.Database.Exists();

            }catch(Exception ex)
            {
                return false;
            }
        }

        public abstract void InsertUser(Users User);

        public abstract bool DeleteUserById(string UserId);

        public abstract bool DeleteUserByProviderName(string ProviderUserName);

        public abstract string GetContainerId(string ProviderUserName);
    }
}