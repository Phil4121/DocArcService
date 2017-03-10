using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using DocArcService.AbstractClasses;

namespace DocArcService.Provider
{
    public class DatabaseProvider : SqlDatabaseProvider
    {
        public override Users GetUserByProviderUserName(string ProviderUserName)
        {
            using(docarcdbEntities ent = new docarcdbEntities())
            {
                return ent.Users.Where(x => x.ProviderUserName == ProviderUserName).FirstOrDefault();
            }
        }
    }
}