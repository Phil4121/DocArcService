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
            var blobStorageConnectionString = GetCloudStorageConnectionString();

            // TODO: Implement GetBlobContainer!
            var blobStorageContainerName = "test";

            var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(blobStorageContainerName);
        }

        public static string GetCloudStorageConnectionString()
        {
            string connString = ConfigurationManager.AppSettings["StorageConnectionString"];

            if (string.IsNullOrEmpty(connString))
                throw new Exception("Connectionstring not found!");

            return connString;
        }
    }
}