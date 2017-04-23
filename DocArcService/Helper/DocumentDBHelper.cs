using DocArcService.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Helper
{
    public static class DocumentDBHelper
    {
        public static async Task<bool> DatabaseExists(DocumentClient Client, string DatabaseId, bool CreateIfNotExists = false)
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null && CreateIfNotExists)
            {
                var databaseDefinition = new Database { Id = DatabaseId };
                await Client.CreateDatabaseAsync(databaseDefinition);

                return true;
            }

            return db == null;
        }

        public static async Task<bool> CollectionExists(DocumentClient Client, string DatabaseId, string CollectionId, bool CreateIfNotExists = false)
        {
            var db = Client.CreateDatabaseQuery()
                .Where(d => d.Id == DatabaseId)
                .AsEnumerable()
                .FirstOrDefault();

            if (db == null)
                throw new Exception("Database does not exists!");

            List<DocumentCollection> cols = Client.CreateDocumentCollectionQuery((string)db.SelfLink).ToList();

            if ((!cols.Any() || !cols.Exists(x => x.Id == CollectionId)) && CreateIfNotExists)
            {

                var collectionDefinition = new DocumentCollection { Id = CollectionId };
                await Client.CreateDocumentCollectionAsync((string)db.SelfLink, collectionDefinition);

                return true;
            }
            else if (cols.Any())
            {
                return cols.Exists(x => x.Id == CollectionId);
            }

            return false;
        }

        public static async Task<bool> SaveDocument(DocumentClient Client, string DatabaseId, string CollectionId, string DocumentId, DocumentModel Document)
        {
            try
            {

                var docExists = Client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                                    .Where(doc => doc.Id == DocumentId)
                                    .Select(doc => doc.Id)
                                    .AsEnumerable()
                                    .Any();

                if (docExists)
                {
                    Uri docUri = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, DocumentId);
                    await Client.ReplaceDocumentAsync(docUri, Document);
                }
                else
                {
                    Uri collUri = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
                    await Client.CreateDocumentAsync(collUri, Document);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}