using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using DocArcService.Provider;
using System.Web;
using System.IO;
using DocArcService.Controllers;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocArcService.Tests
{
    [TestClass]
    public class TestUpload
    {
        [TestMethod]
        public async void TestMockedBlobUpload()
        {
            ProviderFactory.IsMocked = true;

            BlobsController controller = new BlobsController();

            var message = new HttpRequestMessage();

            controller.Request = message;

            await controller.PostBlobUpload();
        }
    }
}
