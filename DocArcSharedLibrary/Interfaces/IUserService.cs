using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(HttpContent httpContent);

        Task<bool> DeleteUserByIdAsync(string id);

        Task<bool> DeleteUserByProviderNameAsync(string providerName);
    }
}
