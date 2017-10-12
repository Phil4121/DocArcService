using DocArcService.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using DocArcService.Models;
using DocArcService.Helper;

namespace DocArcService.Provider
{
    public class DocumentDBProvider : NoSqlDatabaseProvider
    {
        private DocumentClient _Client = null;
        
        public DocumentClient Client
        {
            get
            {
                if (_Client == null)
                    _Client = SetClient();

                return _Client;
            }
        }

        public DocumentDBProvider() : base()
        {

        }

        private DocumentClient SetClient()
        {
            return new DocumentClient(new Uri(base.DbEndpoint), base.DbKey);
        }

        public async override Task<bool> SaveDocument(DocumentModel Document)
        {
            try
            {
                var docDBHelper = new DocumentDBHelper();

                await docDBHelper.DatabaseExists(this.Client, base.DbName, true);

                await docDBHelper.CollectionExists(this.Client, base.DbName, Document.UserId, true);

                await docDBHelper.SaveDocument(this.Client, base.DbName, Document.UserId, Document.FileId, Document);

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}