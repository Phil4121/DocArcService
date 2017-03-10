using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;

namespace DocArcService.AbstractClasses
{
    public abstract class SqlDatabaseProvider : IDatabaseProvider
    {
        private docarcdbEntities _ent;

        public SqlDatabaseProvider()
        {
            _ent = new docarcdbEntities();

            SetConnectionSettings();
        }

        public abstract Users GetUserByProviderUserName(string ProviderUserName);

        public void SetConnectionSettings()
        {
            var formatString = _ent.Database.Connection.ConnectionString.ToString();
            var conString = string.Format(formatString, "docarcdbsrv.database.windows.net", "docarcdb", "dbadmin", "_My2cent_");

            _ent.Database.Connection.ConnectionString = conString;
        }

        public bool DatabaseIsReachable()
        {
            try
            {
                _ent.Database.Connection.Open();
                return _ent.Database.Exists();

            }catch(Exception ex)
            {
                return false;
            }
        }


    }
}