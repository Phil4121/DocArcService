using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DocArcService.Helper
{
    public static class BlobHelper
    {
        public static CloudBlobContainer GetBlobContainer()
        {
            var blobStorageConnectionString = ConfigurationManager.AppSettings["BlobConnectionString"];

            // TODO: Implement GetBlobContainer!
            var blobStorageContainerName = "TEST";

            var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(blobStorageContainerName);
        }
    }
}