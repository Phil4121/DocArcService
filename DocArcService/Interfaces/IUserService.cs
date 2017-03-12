using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(HttpContent httpContent);

        bool DeleteUserById(string id);

        bool DeleteUserByProviderName(string providerName);
    }
}
