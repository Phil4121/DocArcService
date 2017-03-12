using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using DocArcService.AbstractClasses;

namespace DocArcService.Provider
{
    public class AzureDatabaseProvider : SqlDatabaseProvider
    {
        public AzureDatabaseProvider() : base()
        {
        }

        public override bool DeleteUserById(string UserId)
        {
            return DeleteUser(GetUserById(UserId));
        }

        public override bool DeleteUserByProviderName(string ProviderUserName)
        {
            return DeleteUser(GetUserByProviderUserName(ProviderUserName));
        }

        private bool DeleteUser(Users user)
        {
            try
            {
                if (user == null)
                    return false;

                Ent.Users.Remove(user);
                Ent.SaveChanges();
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
                Ent.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}