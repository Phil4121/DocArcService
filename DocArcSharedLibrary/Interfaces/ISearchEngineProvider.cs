using DocArcSharedLibrary.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.Interfaces
{
    public interface ISearchEngineProvider
    {

        Task<bool> UploadToSearchEngine(SearchDocumentModel searchDocument);

        DocumentSearchResult<SearchResultModel> SearchDocumentsForString(string userId, string searchText);
    }
}
