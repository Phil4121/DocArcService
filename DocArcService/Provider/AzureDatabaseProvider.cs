﻿using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using DocArcService.AbstractClasses;
using System.Threading.Tasks;

namespace DocArcService.Provider
{
    public class AzureDatabaseProvider : SqlDatabaseProvider
    {
        public AzureDatabaseProvider() : base()
        {
        }

        #region Users

        public override async Task<bool> DeleteUserByIdAsync(string UserId)
        {
            return await DeleteUser(GetUserById(UserId));
        }

        public override async Task<bool> DeleteUserByProviderNameAsync(string ProviderUserName)
        {
            return await DeleteUser(GetUserByProviderUserName(ProviderUserName));
        }

        private async Task<bool> DeleteUser(Users user)
        {
            try
            {
                if (user == null)
                    return false;

                Ent.Users.Remove(user);
                await Ent.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override string GetContainerId(string ProviderUserName)
        {

            Users user = Ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();

            if (user == null)
                return string.Empty;

            return user.Container;

        }

        public override Users GetUserByProviderUserName(string ProviderUserName)
        {

            return Ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();

        }

        public override Users GetUserById(string UserId)
        {
            return Ent.Users.Where(x => x.UserId == UserId).FirstOrDefault();
        }

        public override void InsertUser(Users User)
        {
            try
            {
                Ent.Users.Add(User);
                Ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Files

        public override void InsertFile(Files file, bool SaveChangesAsyncImed = true)
        {
            try
            {
                Ent.Files.Add(file);

                if(SaveChangesAsyncImed)
                    Ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void InsertFiles(List<Files> files)
        {
            try
            {
                foreach(Files file in files)
                {
                    InsertFile(file, false);
                }

                Ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<bool> DeleteFileAsync(Files file, bool SaveChangesAsyncImed = true)
        {
            try
            {
                Ent.Files.Remove(file);

                if (SaveChangesAsyncImed)
                    await Ent.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<bool> DeleteFilesAsync(List<Files> files)
        {
            try
            {
                foreach (Files file in files)
                {
                    await DeleteFileAsync(file, false);
                }

                await Ent.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<bool> DeleteAllFilesFromUserAsync(string UserId)
        {
            var files = GetFilesByUserId(UserId);
            return await DeleteFilesAsync(files);
        }

        public override List<Files> GetFilesByUserId(string UserId)
        {
            return Ent.Files.Where(x => x.UserId == UserId).ToList();
        }

        public override List<Files> GetFilesByContainerId(string ContainerId)
        {
            return Ent.Files.Where(x => x.Container == ContainerId).ToList();
        }

        #endregion
    }
}