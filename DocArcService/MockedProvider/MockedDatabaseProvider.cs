using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;

namespace DocArcService.MockedProvider
{
    public class MockedDatabaseProvider : IDatabaseProvider
    {
        public Users GetUserByProviderUserName(string ProviderUserName)
        {
            Users user = new Users();
            user.UserId = Guid.NewGuid().ToString();
            user.ProviderUserName = "111010103234";
            user.Email = "test@test.com";
            user.Container = "111-000-222-333";

            return user;
        }

        public bool DatabaseIsReachable()
        {
            return true;
        }
    }
}