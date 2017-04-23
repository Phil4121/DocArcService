using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocArcService.Controllers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Net.Http.Headers;
using DocArcService.Provider;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using DocArcService.Models;
using System.Security.Principal;
using System.Threading;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestUpload
    {
        [TestMethod]
        public async Task Mocked_TestUpload()
        {
            FileStream fileStream = null;

            try
            {
                ProviderFactory.IsMocked = true;

                var controller = new BlobsController();

                var message = new HttpRequestMessage();
                var content = new MultipartFormDataContent();

                var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "Test.txt");

                fileStream = new FileStream(filePath, FileMode.Open);

                content.Add(new StreamContent(fileStream), "file", "Test2");

                message.Method = HttpMethod.Post;
                message.Content = content;

                controller.Request = message;

                var result = await controller.PostBlobUpload();

                Assert.IsInstanceOfType(result.GetType(), typeof(OkResult).GetType());
            }
            finally
            {
                fileStream.Close();
            }
        }

        [TestMethod]
        public async Task TestUpload()
        {
            FileStream fileStream = null;

            try
            {
                ProviderFactory.IsMocked = false;

                var controller = new BlobsController();

                Users testUser = new Users();
                testUser.UserId = Guid.NewGuid().ToString();
                testUser.ProviderUserName = controller.User.Identity.Name;
                testUser.Email = "TestUpload@user.com";
                testUser.Container = Guid.NewGuid().ToString();

                var dbProvider = ProviderFactory.CreateDatabaseProvider();
                await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);

                dbProvider.InsertUser(testUser);

                var message = new HttpRequestMessage();
                var content = new MultipartFormDataContent();

                var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "Test.txt");

                fileStream = new FileStream(filePath, FileMode.Open);

                var sc = new StreamContent(fileStream);
                sc.Headers.Add("Content-Type", "image/jpeg");

                content.Add(sc, "file", "Testfile");

                message.Method = HttpMethod.Post;
                message.Content = content;

                controller.Request = message;

                await controller.CreateBlobContainer(testUser.Container);

                var result = await controller.PostBlobUpload();

                Assert.IsTrue(result is OkNegotiatedContentResult<List<BlobUploadModel>>);
            }
            finally
            {
                fileStream.Close();
            }
        }
    }


}
