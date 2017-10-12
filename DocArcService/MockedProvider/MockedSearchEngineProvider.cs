using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.Search.Models;

namespace DocArcService.MockedProvider
{
    public class MockedSearchEngineProvider : ISearchEngineProvider
    {
        public DocumentSearchResult<SearchResultModel> SearchDocumentsForString(string userId, string searchText)
        {
            var mockedDocSearchResult = new DocumentSearchResult<SearchResultModel>();

            return mockedDocSearchResult;
        }

        public Task<bool> UploadToSearchEngine(SearchDocumentModel searchDocument)
        {
            Thread.Sleep(2000);
            return Task.FromResult(true);
        }
    }
}