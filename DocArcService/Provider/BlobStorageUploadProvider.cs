using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DocArcService.Helper;
using DocArcService.AbstractClasses;

namespace DocArcService.Provider
{
    public class BlobStorageUploadProvider : StorageUploadProvider
    {
        public BlobStorageUploadProvider(string containerName, string providerUserName) : base(containerName, providerUserName)
        {
            Uploads = new List<BlobUploadModel>();
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach(var fileData in FileData)
            {
                try { 
                    var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));

                    var blobContainer = BlobHelper.GetBlobContainer(ContainerName).Result;

                    var blob = blobContainer.GetBlockBlobReference(fileName);

                    blob.Properties.ContentType = fileData.Headers.ContentType.MediaType;

                    using (var fs = File.OpenRead(fileData.LocalFileName))
                    {
                        blob.UploadFromStream(fs);
                    }

                    SaveFileDescriptionToDatabase(fileName, ProviderUserName, ContainerName, blob.Properties.ContentType, blob.Properties.Length);

                    File.Delete(fileData.LocalFileName);

                    var blobUpload = new BlobUploadModel
                    {
                        Container = ContainerName,
                        FileName = blob.Name,
                        FileSizeInBytes = blob.Properties.Length,
                        FileSizeInKb = blob.Properties.Length / 1000,
                        FileContentType = blob.Properties.ContentType
                    };

                    Uploads.Add(blobUpload);

                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return base.ExecutePostProcessingAsync();
        }

        private void SaveFileDescriptionToDatabase(string fileName, string providerUserName, string containerName, string contentType, long fileSizeInBytes)
        {
            var provider = ProviderFactory.CreateDatabaseProvider();

            var file = new Files();
            file.FileId = Guid.NewGuid().ToString();
            file.UserId = provider.GetUserByProviderUserName(providerUserName).UserId;
            file.Container = containerName;
            file.FileSizeInKB = Convert.ToInt32(fileSizeInBytes / 1000);
            file.OriginalFileName = fileName;
            file.OriginalFileType = contentType;

            provider.InsertFile(file);
        }
    }
}