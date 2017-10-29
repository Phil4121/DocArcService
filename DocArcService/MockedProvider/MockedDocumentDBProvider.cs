using DocArcService.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcSharedLibrary.Models;
using System.Threading.Tasks;

namespace DocArcService.MockedProvider
{
    public class MockedDocumentDBProvider : NoSqlDatabaseProvider
    {
        public async override Task<bool> SaveDocument(DocumentModel Document)
        {
            return await Task.FromResult(true);
        }
    }
}