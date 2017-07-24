using DocArcService.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DocArcService.Classes;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestImageOperations
    {
        [TestMethod]
        public void TestChangeImageSize()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "ocr.jpg");

            var imgHelper = new ImageHelper();
      
            using (var image = Image.FromFile(filePath))
            {
                var result = imgHelper.Resize(image, ImageHelper.Format.A4);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task TestImageProcessing()
        {
            var fileName = "Testfile28b9d60a-2c58-4235-947c-5ee5458c0bbc.jpg";
            var container = "7aa14637-7910-4a8c-a33a-48f10b330875";

            var downloadFolder = @"C:\Users\Phil-PC\Documents\Visual Studio 2015\Projects\DocArcService\TestDocArcService\bin\Debug\Testfiles";

            var blobService = new BlobService();

            Assert.IsTrue(await blobService.BlobExists(fileName, container));

            var helper = new FileHelper();
            var location = await helper.GetBlobFromAzureStorage(fileName, container, downloadFolder);

            Assert.IsTrue(File.Exists(location));

            var imageOperationHelper = new ImageHelper();

            using(var img = Image.FromFile(location))
            {
                var newImg = imageOperationHelper.Resize(img, ImageHelper.Format.A4);

                Assert.IsTrue(newImg.Height == 1024 && newImg.Width == 786);
            }
        }
    }
}
