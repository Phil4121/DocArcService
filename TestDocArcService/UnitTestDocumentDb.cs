using DocArcService.Models;
using DocArcService.Provider;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestDocumentDB
    {
        [TestMethod]
        public async Task TestSaveDocument()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDocumentDBProvider();

            DocumentModel model = new DocumentModel();
            model.UserId = "abcd-1234-efgh-5678";
            model.FileId = "9876-iuzt-5432-fghn-1234";

            OcrResults ocr = new OcrResults();

            model.Ocr = ocr;

            Assert.IsTrue(await dbProvider.SaveDocument(model));
        }
    }
}
