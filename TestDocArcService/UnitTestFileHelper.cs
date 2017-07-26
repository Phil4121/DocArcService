using DocArcService.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestFileHelper
    {
        [TestMethod]
        public async Task TestDownload()
        {
            var fileName = "Testfilec79c8947-d193-43ca-9747-daa1cec8e819.jpg";
            var container = "7aa14637-7910-4a8c-a33a-48f10b330875";

            var downloadFolder = @"C:\Users\Phil-PC\Documents\Visual Studio 2015\Projects\DocArcService\TestDocArcService\bin\Debug\Testfiles";

            var helper = new FileHelper();
            var location = await helper.GetBlobFromAzureStorage(fileName, container, downloadFolder);

            Assert.IsTrue(File.Exists(location));

            File.Delete(location);
        }
    }
}
