using DocArcService.Interfaces;
using DocArcService.Models;
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
        public async Task<bool> CreateUser(HttpContent httpContent)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            bool success = await httpContent.ReadAsAsync(typeof(Users))
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                        throw task.Exception;

                    userProvider.InsertUser((Users)task.Result);
                    return true;
                }
                );

            return success;
        }

        public bool DeleteUserById(string id)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            return userProvider.DeleteUserById(id);
        }

        public bool DeleteUserByProviderName(string providerName)
        {
            var userProvider = ProviderFactory.CreateDatabaseProvider();

            return userProvider.DeleteUserByProviderName(providerName);
        }
    }
}