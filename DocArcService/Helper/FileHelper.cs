using DocArcService.Classes;
using DocArcService.Controllers;
using DocArcService.Models;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Auth.Protocol;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Core.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace DocArcService.Helper
{
    public class FileHelper
    {
        public async Task<string> GetBlobFromAzureStorage(string fileName, string container, string downloadFolder)
        {
            var blobService = new BlobService();

            var blobDownload = await blobService.DownloadBlob(fileName, container);

            var downloadPath = FormatDownloadFolder(downloadFolder) + blobDownload.BlobFileName;

            FileStream file = new FileStream(downloadPath, FileMode.Create, FileAccess.Write);

            try
            {
                using (var ms = blobDownload.BlobStream)
                {
                    ms.WriteTo(file);
                    file.Close();
                    ms.Close();

                    return downloadPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                file.Dispose();
            }
        }

        public async Task<bool> BlobExists(string fileName, string container)
        {
            var blobService = new BlobService();

            return await blobService.BlobExists(fileName, container);
        }

        public async Task<bool> UploadBlobToAzureStorage(string fileName, string fileLocation, string container)
        {

            try
            {
                var blobService = new BlobService();
           
                return await blobService.UploadBlob(fileName, fileLocation, container);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private string FormatDownloadFolder(string folder)
        {
            if (!folder.EndsWith("\\"))
                folder += @"\";

            return folder;
        }
    }
}