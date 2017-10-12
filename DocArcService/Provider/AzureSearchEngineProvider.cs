using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using System.Threading.Tasks;
using DocArcService.Helper;
using Microsoft.Azure.Search;
using System.Configuration;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Search.Models;

namespace DocArcService.Provider
{
    public class AzureSearchEngineProvider : ISearchEngineProvider
    {
        private SearchServiceClient _serviceClient;

        private SearchIndexClient _searchIndexClient;

        private string _searchServiceAPIKey = string.Empty;

        private string SearchServiceAPIKey
        {
            get
            {
                if (String.IsNullOrEmpty(_searchServiceAPIKey))
                    _searchServiceAPIKey = GetSearchServiceAPIKey();

                return _searchServiceAPIKey;
            }
        }

        private string _searchServiceName = string.Empty;

        private string SearchServiceName
        {
            get
            {
                if (String.IsNullOrEmpty(_searchServiceName))
                    _searchServiceName = GetSearchServiceName();

                return _searchServiceName;
            }
        }

        private string _indexName = string.Empty;

        private string IndexName
        {
            get
            {
                if (String.IsNullOrEmpty(_indexName))
                    _indexName = GetIndexName();

                return _indexName;
            }
        }

        public SearchServiceClient ServiceClient
        {
            get { return _serviceClient; }
        }

        public SearchIndexClient SearchIndexClient
        {
            get { return _searchIndexClient; }
        }

        public AzureSearchEngineProvider()
        {
            try
            {
                _serviceClient = new SearchServiceClient(SearchServiceName, new SearchCredentials(SearchServiceAPIKey));
                _searchIndexClient = new SearchIndexClient(SearchServiceName, IndexName, new SearchCredentials(SearchServiceAPIKey));
            }
            catch (CloudException ex)
            {
                Console.WriteLine(ex.Message + "; May check API Keys if its 'forbidden'");
                throw;
            }
        }

        public Task<bool> UploadToSearchEngine(SearchDocumentModel searchDocument)
        {
            try
            {
                var azureSearchHelper = new AzureSearchHelper();

                CreateIndexIfNotExists();

                azureSearchHelper.UploadDocuments(SearchIndexClient, searchDocument);

                return Task.FromResult(true);

            }catch(Exception ex)
            {
                return Task.FromResult(false);
            }
        }

        private string GetSearchServiceName()
        {
            if (!Utility.AppSettingExists("SearchServiceName"))
                throw new Exception("SearchServiceName not found in AppSettings!");

            return ConfigurationManager.AppSettings["SearchServiceName"].ToString();
        }

        private string GetSearchServiceAPIKey()
        {
            if (!Utility.AppSettingExists("SearchServiceAPIKey"))
                throw new Exception("SearchServiceAPIKey not found in AppSettings!");

            return ConfigurationManager.AppSettings["SearchServiceAPIKey"].ToString();
        }

        private string GetIndexName()
        {
            if (!Utility.AppSettingExists("SearchServiceIndexName"))
                throw new Exception("SearchServiceIndexName not found in AppSettings!");

            return ConfigurationManager.AppSettings["SearchServiceIndexName"].ToString().ToLower();
        }

        private void CreateIndexIfNotExists()
        {
            if (ServiceClient.Indexes.Exists(GetIndexName()))
                return;

            var definition = new Index()
            {
                Name = GetIndexName(),
                Fields = FieldBuilder.BuildForType<SearchDocumentModel>()
            };

            ServiceClient.Indexes.Create(definition);

        }

        public DocumentSearchResult<SearchResultModel> SearchDocumentsForString(string userId, string searchText)
        {
            try
            {
                var sp = new SearchParameters()
                {
                    Filter = "UserId eq '" + userId + "'"
                };
                

                return this.SearchIndexClient.Documents.Search<SearchResultModel>(searchText, sp);
               

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed search: {0}", e.Message.ToString());

                return null;
            }
        }
    }
}