﻿using DocArcService.Classes;
using DocArcService.Interfaces;
using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DocArcService.Controllers
{
    public class BlobsController : ApiController
    {
        private readonly IBlobService _service = new BlobService();

        /// <summary>
        /// Uploads one or more blob files
        /// </summary>
        /// <returns></returns>
        /// 
        [ResponseType(typeof(List<BlobUploadModel>))]
        public async Task<IHttpActionResult> PostBlobUpload()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                var result = await _service.UploadBlob(Request.Content);

                if(result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                return BadRequest();

            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Downloads a blob file
        /// </summary>
        /// <param name="blobFileName">Filename of the blob</param>
        /// <returns></returns>
        /// 
        public async Task<HttpResponseMessage> GetBlobDownload(string blobFileName)
        {
            try
            {
                var result = await _service.DownloadBlob(blobFileName);

                if(result == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                result.BlobStream.Position = 0;

                var message = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(result.BlobStream)
                };

                message.Content.Headers.ContentLength = result.BlobLength;
                message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(result.BlobContentType);
                message.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = HttpUtility.UrlDecode(result.BlobFileName),
                    Size = result.BlobLength
                };

                return message;
            }catch(Exception ex)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };
            }
        }
    }
}