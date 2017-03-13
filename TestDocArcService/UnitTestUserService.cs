using DocArcService.Controllers;
using DocArcService.Models;
using DocArcService.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestUserService
    {
        [TestMethod]
        public async Task TestCreateUser()
        {
            ProviderFactory.IsMocked = false;

            var controller = new UserController();

            var user = new Users();
            user.Container = Guid.NewGuid().ToString();
            user.UserId = Guid.NewGuid().ToString();
            user.ProviderUserName = "123456789";
            user.Email = "user@test.com";

            controller.DeleteUserByProviderName(user.ProviderUserName);

            var message = new HttpRequestMessage();
            var content = new ObjectContent<Users>(user, new JsonMediaTypeFormatter());

            message.Method = HttpMethod.Post;
            message.Content = content;

            controller.Request = message;

            var result = await controller.CreateUser();

            Assert.IsInstanceOfType(result.GetType(), typeof(OkResult).GetType());
        }
    }
}
