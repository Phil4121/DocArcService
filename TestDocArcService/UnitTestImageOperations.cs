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
    public class UnitTestImageOperations
    {
        [TestMethod]
        public void TestChangeImageSize()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, @"Testfiles\", "ocr.jpg");

            var imgHelper = new ImageHelper();
      
            var result = imgHelper.Resize(filePath, ImageHelper.Format.A4);

            Assert.IsTrue(!string.IsNullOrEmpty(result.ToString()));
        }
    }
}
