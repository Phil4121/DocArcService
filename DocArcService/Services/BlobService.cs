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
        public async Task<List<BlobUploadModel>> UploadBlob(HttpContent httpContent)
        {
            var blobUploadProvider = ProviderFactory.CreateBlobStorageUploadProvider();

            var list = await httpContent.ReadAsMultipartAsync(blobUploadProvider)
                .ContinueWith(task =>
                    {
                        if (task.IsFaulted || task.IsCanceled)
                            throw task.Exception;

                        var provider = task.Result;
                        return provider.Uploads.ToList();
                    }
                );

            // TODO: Use data in the list to store blob infos in my database!

            return list;
        }

        public async Task<BlobDownloadModel> DownloadBlob(string blobFileName)
        {
            if (!String.IsNullOrEmpty(blobFileName))
            {
                var container = BlobHelper.GetBlobContainer();
                var blob = container.GetBlockBlobReference(blobFileName);

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