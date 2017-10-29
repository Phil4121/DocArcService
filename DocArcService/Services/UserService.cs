using DocArcSharedLibrary.Interfaces;
using DocArcSharedLibrary.Models;
using DocArcService.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Services
{
    public class UserService : IUserService
    {
        public async Task<bool> CreateUserAsync(HttpContent httpContent)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            bool success = await httpContent.ReadAsAsync(typeof(UserModel))
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                        throw task.Exception;

                    userProvider.AddUser((UserModel)task.Result);
                    return true;
                }
                );

            return success;
        }

        public async Task<bool> DeleteUserByIdAsync(string id)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            return await userProvider.DeleteUserByIdAsync(id);
        }

        public async Task<bool> DeleteUserByProviderNameAsync(string providerName)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            return await userProvider.DeleteUserByProviderNameAsync(providerName);
        }
    }
}