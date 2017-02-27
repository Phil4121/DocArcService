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

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestUpload
    {
        [TestMethod]
        public async Task TestUpload()
        {
            ProviderFactory.IsMocked = true;

            var controller = new BlobsController();

            var message = new HttpRequestMessage();
            var content = new MultipartFormDataContent();

            var filestream = new FileStream("C:\\Temp\\Test.txt", FileMode.Open);
            content.Add(new StreamContent(filestream), "file", "Test2");

            message.Method = HttpMethod.Post;
            message.Content = content;

            controller.Request = message;

            var result = await controller.PostBlobUpload();

            Assert.IsInstanceOfType(result.GetType(), typeof(OkResult).GetType());
        }
    }


}
