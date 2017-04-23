using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcService.Models
{
    public class DocumentModel
    {
        public string UserId { get; set; }

        public string FileId { get; set; }

        public OcrResults Ocr { get; set; }
    }
}