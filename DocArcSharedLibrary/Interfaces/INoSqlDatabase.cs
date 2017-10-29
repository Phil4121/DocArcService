using DocArcSharedLibrary.Models;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcSharedLibrary.Interfaces
{
    public interface INoSqlDatabase
    {
        Task<bool> SaveDocument(DocumentModel Document);
    }
}