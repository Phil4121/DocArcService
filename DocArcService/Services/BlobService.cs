using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using DocArcService.Provider;
using DocArcService.Helper;
using DocArcService.AbstractClasses;

namespace DocArcService.Classes
{
    public class BlobService : IBlobService
    {
        public async Task<List<BlobUploadModel>> UploadBlob(HttpContent httpContent, string container, string providerUserName)
        {
            var blobUploadProvider = ProviderFactory.CreateBlobStorageUploadProvider(container, providerUserName);

            var list = await httpContent.ReadAsMultipartAsync(blobUploadProvider)
                .ContinueWith(task =>
                    {
                        if (task.IsFaulted || task.IsCanceled)
                            throw task.Exception;

                        var provider = task.Result;
                        return provider.Uploads.ToList();
                    }
                );

            return list;
        }

        public async Task<bool> CreateBlobContainer(string container)
        {
            var blobContainer = await BlobHelper.GetBlobContainer(container, true);

            return blobContainer != null;
        } 

        public async Task<BlobDownloadModel> DownloadBlob(string blobFileName, string container)
        {
            if (!String.IsNullOrEmpty(blobFileName))
            {
                var cont = await BlobHelper.GetBlobContainer(container);
                var blob = cont.GetBlockBlobReference(blobFileName);

                var ms = new MemoryStream();
                await blob.DownloadToStreamAsync(ms);

                var download = new BlobDownloadModel
                {
                    BlobStream = ms,
                    BlobFileName = blobFileName,
                    BlobLength = blob.Properties.Length,
                    BlobContentType = blob.Properties.ContentType
                };

                return download;
            }

            return null;
        }
    }
}