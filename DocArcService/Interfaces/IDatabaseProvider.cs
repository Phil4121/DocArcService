using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    public interface IDatabaseProvider
    {
        Users GetUserByProviderUserName(string ProviderUserName);

        Users GetUserById(string UserId);

        void InsertUser(Users User);

        bool DeleteUserById(string UserId);

        bool DeleteUserByProviderName(string ProviderUserName);

        string GetContainerId(string ProviderUserName);

        bool DatabaseIsReachable();
    }
}
