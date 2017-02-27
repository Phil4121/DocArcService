using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using DocArcService.Models;
using System.IO;

namespace DocArcService.AbstractClasses
{
    public abstract class StorageUploadProvider : MultipartFileStreamProvider, IBlobStorageUploadProvider
    {
        public List<BlobUploadModel> Uploads { get; set; }

        public StorageUploadProvider() : base(Path.GetTempPath())
        {

        }
    }
}