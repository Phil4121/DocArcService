using DocArcService.Interfaces;
using DocArcService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DocArcService.Controllers
{
    public class CognitiveController : ApiController
    {
        private readonly ICognitiveService _service = new CognitiveServicesVisionService();

        /// <summary>
        /// Processes Blob throw CognitiveService
        /// </summary>
        /// <returns></returns>
        /// 
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> ProzessBlobFile(string containerName, string blobFileName)
        {
            try
            {
                var result = await _service.ProzessImageAsync(containerName, blobFileName);

                if (result)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
