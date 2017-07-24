using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocArcService.Models
{
    public class BlobUploadModel
    {
        public string Container { get; set; }

        public string FileName { get; set; }

        public string FileContentType { get; set; }

        public long FileSizeInBytes { get; set; }

        public long FileSizeInKb { get; set; }
    }
}