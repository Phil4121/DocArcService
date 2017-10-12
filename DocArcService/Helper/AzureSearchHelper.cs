using DocArcService.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcService.Helper
{
    public class AzureSearchHelper
    {
        public void CreateIndex(SearchServiceClient serviceClient, string indexName)
        {

            if (serviceClient.Indexes.Exists(indexName))
            {
                //serviceClient.Indexes.Delete(indexName);
                return;
            }

            var definition = new Index()
            {
                Name = indexName,
                Fields = new[]
                {
                    new Field("FileId", DataType.String)                       { IsKey = true },
                    new Field("UserId", DataType.String)                     { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false },
                    new Field("Ocr", DataType.String)                      { IsSearchable = true, IsFilterable = false, IsSortable = false, IsFacetable = false }
                }
            };

            serviceClient.Indexes.Create(definition);
        }

        public void UploadDocuments(SearchIndexClient indexClient, SearchDocumentModel documentModel)
        {
            List<IndexAction> indexOperations = new List<IndexAction>();
            var doc = new Document();
            doc.Add("FileId", documentModel.FileId);
            doc.Add("UserId", documentModel.UserId);
            doc.Add("Ocr", documentModel.Ocr);
            indexOperations.Add(IndexAction.Upload(doc));

            try
            {
                indexClient.Documents.Index(new IndexBatch(indexOperations));
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                "Failed to index some of the documents: {0}",
                       String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

        }


        public List<Models.SearchResultModel> SearchDocuments(SearchIndexClient indexClient, string searchText)
        {
            var searchResults = new List<Models.SearchResultModel>();
            try
            {
                var sp = new SearchParameters();

                var response = indexClient.Documents.Search(searchText, sp);

                foreach (var result in response.Results)
                {
                    //var document = (AzureSearchResult) result.Document;

                    searchResults.Add(new Models.SearchResultModel(result.Document["FileId"].ToString(), result.Document["UserId"].ToString(), result.Document["Ocr"].ToString()));

                    //Console.WriteLine("File ID: {0}", result.Document.FileId);
                    //Console.WriteLine("User ID: {0}", result.Document.UserId);
                    //Console.WriteLine("Extracted Text: {0}", result.Document.Ocr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed search: {0}", e.Message.ToString());
            }

            return searchResults;
        }
    }
}