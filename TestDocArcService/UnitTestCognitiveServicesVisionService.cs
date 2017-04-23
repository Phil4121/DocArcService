using DocArcService.Services;
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
    public class UnitTestCognitiveServicesVisionService
    {
        [TestMethod]
        public async Task TestImageToText()
        {
            var visionService = new CognitiveServicesVisionService();

            var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "ocr.jpg");

            var result = await visionService.ConvertImageToTextAsync(filePath.ToString());

            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()));
        }
    }
}
