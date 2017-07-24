using DocArcService.Interfaces;
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
            return await DeleteUser(GetDbUserById(UserId));
        }

        public override async Task<bool> DeleteUserByProviderNameAsync(string ProviderUserName)
        {
            return await DeleteUser(GetDbUserByProviderUserName(ProviderUserName));
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override string GetContainerId(string ProviderUserName)
        {

            Users user = Ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();

            if (user == null)
                return string.Empty;

            return user.Container;

        }

        public override UserModel GetUserByProviderUserName(string ProviderUserName)
        {
            try
            {
                if (ProviderUserName == string.Empty)
                    throw new Exception("No ProviderUserName submitted");

                var dbUser = Ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();

                if (dbUser == null)
                    return new UserModel();

                return MapUserToUserModel(dbUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserModel();
            }
        }

        public override UserModel GetUserById(string UserId)
        {
            try
            {
                if (UserId == string.Empty)
                    throw new Exception("No UserId submitted");

                var dbUser = Ent.Users.Where(x => x.UserId == UserId).FirstOrDefault();

                if (dbUser == null)
                    return new UserModel();

                return MapUserToUserModel(dbUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserModel();
            }
        }

        private Users GetDbUserById(string UserId)
        {
            try
            {
                if (UserId == string.Empty)
                    throw new Exception("No UserId submitted");

                return Ent.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Users();
            }
        }

        private Users GetDbUserByProviderUserName(string ProviderUserName)
        {
            try
            {
                if (ProviderUserName == string.Empty)
                    throw new Exception("No ProviderUserName submitted");

                return Ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserModel();
            }
        }

        private UserModel MapUserToUserModel(Users user)
        {
            var userModel = new UserModel(user.UserId, user.Container);
            userModel.ProviderUserName = user.ProviderUserName;
            userModel.Email = user.Email;
            userModel.Files = user.Files;

            return userModel;
        }


        public override UserModel AddUser(UserModel User)
        {
            try
            {
                if (UserExists(User.ProviderUserName))
                    return GetUserByProviderUserName(User.ProviderUserName);


                var dbUser = new Users();
                dbUser.UserId = Guid.NewGuid().ToString();
                dbUser.Container = Guid.NewGuid().ToString();
                dbUser.Email = User.Email;
                dbUser.ProviderUserName = User.ProviderUserName;

                Ent.Users.Add(dbUser);
                Ent.SaveChanges();

                return MapUserToUserModel(dbUser);
            }
            catch (Exception)
            {
                return new UserModel();
            }
        }

        private bool UserExists(string ProviderUserName)
        {
            var user = GetUserByProviderUserName(ProviderUserName);
            return user != null && user.UserId != string.Empty;
        }

        #endregion

        #region Files

        public override bool InsertFile(Files file, bool SaveChangesImed = true)
        {
            try
            {
                Ent.Files.Add(file);

                if(SaveChangesImed)
                    Ent.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override async Task<bool> InsertFiles(List<Files> files)
        {
            try
            {
                foreach(Files file in files)
                {
                    InsertFile(file, false);
                }

                await Ent.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
                return false;
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
                return false;
            }
        }

        public override async Task<bool> DeleteAllFilesFromUserAsync(string UserId)
        {
            try
            {
                if (UserId == string.Empty)
                    throw new Exception("No UserId submitted");

                var files = GetFilesByUserId(UserId);
                return await DeleteFilesAsync(files);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override List<Files> GetFilesByUserId(string UserId)
        {
            try
            {
                if (UserId == string.Empty)
                    throw new Exception("No UserId submitted");

                return Ent.Files.Where(x => x.UserId == UserId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Files>();
            }
        }

        public override List<Files> GetFilesByContainerId(string ContainerId)
        {
            try
            {
                if (ContainerId == string.Empty)
                    throw new Exception("No ContainerId submitted");

                return Ent.Files.Where(x => x.Container == ContainerId).ToList<Files>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Files>();
            }
        }

        #endregion
    }
}