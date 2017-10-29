using Microsoft.Azure.Search;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcSharedLibrary.Models
{
    [Serializable]
    public class SearchResultModel
    {
        private string _FileId;

        private string _UserId;

        private string _Ocr; 


        public SearchResultModel(string fileId, string userId, string ocr)
        {
            _FileId = fileId;
            _UserId = userId;
            _Ocr = ocr;
        }

        public string FileId {
            get
            {
                return _FileId;
            }
        }

        public string UserId {
            get {
                return _UserId;
            }
        }

        public string ExtractedText {
            get
            {
                return _Ocr;
            }
        }
    }
}