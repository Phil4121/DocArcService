using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using DocArcService.Helper;
using DocArcService.Interfaces;
using DocArcService.AbstractClasses;

namespace DocArcService.Provider
{
    public class BlobStorageUploadProvider : StorageUploadProvider
    {
        public BlobStorageUploadProvider()
        {
            Uploads = new List<BlobUploadModel>();
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach(var fileData in FileData)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));

                var blobContainer = BlobHelper.GetBlobContainer();
                var blob = blobContainer.GetBlockBlobReference(fileName);

                blob.Properties.ContentType = fileData.Headers.ContentType.MediaType;

                using (var fs = File.OpenRead(fileData.LocalFileName))
                {
                    blob.UploadFromStream(fs);
                }

                File.Delete(fileData.LocalFileName);

                var blobUpload = new BlobUploadModel
                {
                    FileName = blob.Name,
                    FileSizeInBytes = blob.Properties.Length
                };

                Uploads.Add(blobUpload);
            }

            return base.ExecutePostProcessingAsync();
        }
    }
}