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

        bool DatabaseIsReachable();
    }
}
