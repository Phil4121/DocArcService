using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    public interface IBlobService
    {
        Task<List<BlobUploadModel>> UploadBlob(HttpContent httpContent, string container);

        Task<bool> CreateBlobContainer(string container);

        Task<BlobDownloadModel> DownloadBlob(string blobFileName, string container);
    }
}
