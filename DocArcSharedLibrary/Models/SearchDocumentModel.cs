using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcSharedLibrary.Models
{
    [Serializable]
    public class SearchDocumentModel
    {
        private string _FileId;

        private string _UserId;

        private string _Ocr; 


        public SearchDocumentModel(string fileId, string userId, string ocr)
        {
            _FileId = fileId;
            _UserId = userId;
            _Ocr = ocr;
        }

        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable, IsSearchable]
        public string FileId {
            get
            {
                return _FileId;
            }
        }

        [IsFilterable, IsSearchable]
        public string UserId {
            get {
                return _UserId;
            }
        }

        [IsFilterable, IsSearchable]
        public string Ocr {
            get
            {
                return _Ocr;
            }
        }
    }
}