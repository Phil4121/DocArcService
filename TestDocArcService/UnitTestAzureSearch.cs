using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search;
using DocArcService.Helper;
using System.Configuration;
using DocArcService.Models;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading;
using DocArcService.Provider;
using Microsoft.Azure.Search.Models;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestAzureSearch
    {
        [TestMethod]
        public void TestAzureSearch()
        {
            var searchProvider = ProviderFactory.CreateSearchEngineProvider();

            string ocrText = "Damit Ihr indess erkennt, woher dieser ganze Irrthum gekommen ist, " +
                 "und weshalb man die Lust anklagt und den Schmerz lobet, so will ich Euch Alles " +
                 "eröffnen und auseinander setzen, was jener Begründer der Wahrheit und gleichsam " +
                 "Baumeister des glücklichen Lebens selbst darüber gesagt hat. Niemand, sagt er, " +
                 "verschmähe, oder hasse, oder fliehe die Lust als solche, sondern weil grosse " +
                 "Schmerzen ihr folgen, wenn man nicht mit Vernunft ihr nachzugehen verstehe. " +
                 "Ebenso werde der Schmerz als solcher von Niemand geliebt, gesucht und verlangt, " +
                 "sondern weil mitunter solche Zeiten eintreten, dass man mittelst Arbeiten und " +
                 "Schmerzen eine grosse Lust sich zu verschaften suchen müsse. Um hier gleich bei " +
                 "dem Einfachsten stehen zu bleiben, so würde Niemand von uns anstrengende körperliche " +
                 "Uebungen vornehmen, wenn er nicht einen Vortheil davon erwartete. Wer dürfte aber wohl " +
                 "Den tadeln, der nach einer Lust verlangt, welcher keine Unannehmlichkeit folgt, oder " +
                 "der einem Schmerze ausweicht, aus dem keine Lust hervorgeht? Forelle";


            Console.WriteLine("Uploading extracted text to Azure Search...");
            string userId = "1234-5678-910";
            string fileId = Guid.NewGuid().ToString();

            var searchDoc = new SearchDocumentModel(fileId, userId, ocrText);

            searchProvider.UploadToSearchEngine(searchDoc);

            // wait because upload and processing last a few seconds
            Thread.Sleep(10000);

            // Execute a test search 
            Console.WriteLine("Execute Search...");

            var results = searchProvider.SearchDocumentsForString(userId, "keine Lust");

            Assert.IsNotNull(results);

            foreach (SearchResult<SearchResultModel> result in results.Results)
            {
                Console.WriteLine("File ID: {0}", result.Document.FileId);
                Console.WriteLine("User ID: {0}", result.Document.UserId);
                Console.WriteLine("Extracted Text: {0}", result.Document.ExtractedText);
            }

            Assert.IsTrue(results.Results.Count > 0);
        }
    }
}
