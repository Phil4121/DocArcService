﻿using DocArcSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcSharedLibrary.Models;
using System.Configuration;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.AbstractClasses
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

        public bool DatabaseIsReachable()
        {
            try
            {
                Ent.Database.Connection.Open();
                return Ent.Database.Exists();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region Users

        public abstract UserModel GetUserByProviderUserName(string ProviderUserName);

        public abstract UserModel GetUserById(string UserId);

        public abstract UserModel AddUser(UserModel User);

        public abstract Task<bool> DeleteUserByIdAsync(string UserId);

        public abstract Task<bool> DeleteUserByProviderNameAsync(string ProviderUserName);

        public abstract string GetContainerId(string ProviderUserName);

        #endregion

        #region Files

        public abstract bool InsertFile(Files file, bool SaveChangesAsyncImed = true);

        public abstract Task<bool> InsertFiles(List<Files> files);

        public abstract Task<bool> DeleteFileAsync(Files file, bool SaveChangesAsyncImed = true);

        public abstract Task<bool> DeleteFilesAsync(List<Files> files);

        public abstract Task<bool> DeleteAllFilesFromUserAsync(string UserId);

        public abstract List<Files> GetFilesByUserId(string UserId);

        public abstract List<Files> GetFilesByContainerId(string ContainerId);

        #endregion


    }
}