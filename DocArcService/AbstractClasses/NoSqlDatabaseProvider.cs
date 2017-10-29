using DocArcSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;
using System.Configuration;
using DocArcService.Helper;
using DocArcSharedLibrary.Models;

namespace DocArcService.AbstractClasses
{
    public abstract class NoSqlDatabaseProvider : INoSqlDatabase
    {
        private string _DbKey = string.Empty;
        private string _DbEndpoint = string.Empty;
        private string _DbName = string.Empty;

        public string DbKey
        {
            get
            {
                if (String.IsNullOrEmpty(_DbKey))
                    _DbKey = GetDbKey();

                return _DbKey;
            }
        }

        public string DbEndpoint
        {
            get
            {
                if (string.IsNullOrEmpty(_DbEndpoint))
                    _DbEndpoint = GetDbEndpoint();

                return _DbEndpoint;
            }
        }

        public string DbName
        {
            get
            {
                if (string.IsNullOrEmpty(_DbName))
                    _DbName = GetDbName();

                return _DbName;
            }
        }

        private string GetDbKey()
        {
            return ConfigurationManager.AppSettings["DocumentDbKey"].ToString();
        }

        private string GetDbEndpoint()
        {
            return ConfigurationManager.AppSettings["DocumentDbEndpoint"].ToString();
        }

        private string GetDbName()
        {
            return ConfigurationManager.AppSettings["DocumentDbName"].ToString();
        }

        #region Documents

        public abstract Task<bool> SaveDocument(DocumentModel Document);

        #endregion
    }
}