using DocArcService.Helper;
using DocArcService.Interfaces;
using DocArcService.Provider;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Services
{
    public class CognitiveServicesVisionService : ICognitiveService
    {
        public async Task<bool> ProzessImageAsync(string blobFileName, string container)
        {
            try
            {
                var fileProcessingProvider = ProviderFactory.CreateFileProcessingProvider();

                return await fileProcessingProvider.ProzessImage(blobFileName, container);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}