using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Helper
{
    public static class BlobHelper
    {
        public async static Task<CloudBlobContainer> GetBlobContainer(string containerName, bool createIfNotExists = false)
        {
            if (!IsValidContainerName(containerName))
                throw new Exception("ContainerName not valid!");

            var blobStorageConnectionString = GetCloudStorageConnectionString();

            var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();

            if (createIfNotExists) { 
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                //Create a new container, if it does not exist
                await container.CreateIfNotExistsAsync();
            }

            if(! await BlobContainerExists(containerName))
                throw new Exception("Container does not exists");

            return blobClient.GetContainerReference(containerName);
        }

        public async static Task<bool> BlobContainerExists(string containerName)
        {
            if (!IsValidContainerName(containerName))
                throw new Exception("ContainerName not valid!");

            var blobStorageConnectionString = GetCloudStorageConnectionString();

            var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            return await container.ExistsAsync();
        }

        public static string GetCloudStorageConnectionString()
        {
            string connString = ConfigurationManager.AppSettings["StorageConnectionString"];

            if (string.IsNullOrEmpty(connString))
                throw new Exception("Connectionstring not found!");

            return connString;
        }

        private static bool IsValidContainerName(string containerName)
        {
            Regex rgx = new Regex(@"^[a-z0-9](?!.*--)[a-z0-9-]{1,61}[a-z0-9]$");

            return rgx.IsMatch(containerName);
        }
    }
}