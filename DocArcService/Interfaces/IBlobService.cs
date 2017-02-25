using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    interface IBlobService
    {
        Task<List<BlobUploadModel>> UploadBlob(HttpContent httpContent);
        Task<BlobDownloadModel> DownloadBlob(string blobFileName);
    }
}
