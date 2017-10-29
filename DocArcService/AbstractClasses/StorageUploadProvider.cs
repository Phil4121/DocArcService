using DocArcSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using DocArcSharedLibrary.Models;
using System.IO;

namespace DocArcService.AbstractClasses
{
    public abstract class StorageUploadProvider : MultipartFileStreamProvider, IBlobStorageUploadProvider
    {
        public List<BlobUploadModel> Uploads { get; set; }

        public string ContainerName { get; }

        public string ProviderUserName { get; }

        public StorageUploadProvider(string ContainerName, string ProviderUserName) : base(Path.GetTempPath())
        {
            this.ContainerName = ContainerName;
            this.ProviderUserName = ProviderUserName;
        }
    }
}