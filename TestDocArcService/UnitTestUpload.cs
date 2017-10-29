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
using DocArcSharedLibrary.Models;
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

                UserModel testUser = new UserModel();
                testUser.ProviderUserName = controller.User.Identity.Name;
                testUser.Email = "TestUpload@user.com";
                var dbProvider = ProviderFactory.CreateDatabaseProvider();
                try
                {
                    await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);
                }catch(Exception ex)
                {
                    //maybe problem with db server firewall! Add you IP Address
                    Console.WriteLine(ex.Message);
                }
                dbProvider.AddUser(testUser);

                testUser = dbProvider.GetUserByProviderUserName(controller.User.Identity.Name);

                var message = new HttpRequestMessage();
                var content = new MultipartFormDataContent();

                var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "ocr.jpg");

                fileStream = new FileStream(filePath, FileMode.Open);

                var sc = new StreamContent(fileStream);
                sc.Headers.Add("Content-Type", "image/jpeg");

                content.Add(sc, "file", "Testfile" + Guid.NewGuid() + ".jpg");

                message.Method = HttpMethod.Post;
                message.Content = content;

                controller.Request = message;

                await controller.CreateBlobContainer(testUser.Container);

                var result = await controller.PostBlobUpload();

                Assert.IsTrue(result is OkNegotiatedContentResult<List<BlobUploadModel>>);
            }catch(Exception ex)
            {
                Console.WriteLine("************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("************************************");
            }
            finally
            {
                fileStream.Close();
            }
        }
    }


}
