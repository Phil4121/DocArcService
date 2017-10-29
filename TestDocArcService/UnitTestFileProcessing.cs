using DocArcService.Controllers;
using DocArcSharedLibrary.Interfaces;
using DocArcSharedLibrary.Models;
using DocArcService.Provider;
using DocArcService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestFileProcessing
    {
        [TestMethod]
        public async Task TestFileProcessing()
        {
            try
            {
                IFileProcessingProvider provider = new CognitiveServiceProvider();

                OkNegotiatedContentResult<List<BlobUploadModel>> result = await TestFileUpload();

                Assert.IsNotNull(result);

                var blob = result.Content.FirstOrDefault();

                Assert.IsNotNull(blob);

                if (blob == null)
                    return;

                var ok = await provider.ProzessImage(blob.FileName, blob.Container);

                Assert.IsTrue(ok);
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<OkNegotiatedContentResult<List<BlobUploadModel>>> TestFileUpload()
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
                }
                catch (Exception ex)
                {
                    //maybe problem with db server firewall! Add you IP Address
                    Console.WriteLine(ex.Message);
                }
                dbProvider.AddUser(testUser);

                var message = new HttpRequestMessage();
                var content = new MultipartFormDataContent();

                var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "ocr.jpg");

                fileStream = new FileStream(filePath, FileMode.Open);

                var sc = new StreamContent(fileStream);
                sc.Headers.Add("Content-Type", "image/jpeg");

                content.Add(sc, "file", "Testfile" + Guid.NewGuid().ToString());

                message.Method = HttpMethod.Post;
                message.Content = content;

                controller.Request = message;

                await controller.CreateBlobContainer(testUser.Container);

                var result = await controller.PostBlobUpload();

                Assert.IsTrue(result is OkNegotiatedContentResult<List<BlobUploadModel>>);

                return (OkNegotiatedContentResult<List<BlobUploadModel>>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("************************************");

                return null;
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}
