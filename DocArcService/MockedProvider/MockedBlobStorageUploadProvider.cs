using DocArcService.AbstractClasses;
using DocArcService.Interfaces;
using DocArcService.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.MockedProvider
{
    public class MockedBlobStorageUploadProvider : StorageUploadProvider
    {
        public MockedBlobStorageUploadProvider(string containerName, string providerUserName) : base(containerName, providerUserName) 
        {
            Uploads = new List<BlobUploadModel>();
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach (var fileData in FileData)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));

                var rnd = new Random();

                var blobUpload = new BlobUploadModel
                {
                    Container = "MockedContainer",
                    FileName = fileName,
                    FileSizeInBytes = rnd.Next(int.MinValue, int.MaxValue)
                };

                Uploads.Add(blobUpload);
            }

            return base.ExecutePostProcessingAsync();
        }
    }
}