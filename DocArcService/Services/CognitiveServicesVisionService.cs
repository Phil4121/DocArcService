using DocArcService.Helper;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Services
{
    public class CognitiveServicesVisionService
    {
        private string _SubscriptionKey = string.Empty;

        private string SubstcriptionKey
        {
            get
            {
                if (String.IsNullOrEmpty(_SubscriptionKey))
                    _SubscriptionKey = GetSubscriptionKey();

                return _SubscriptionKey;
            }
        }


        public async Task<OcrResults> ConvertImageToTextAsync(string ImageFilePath)
        {
            var cognitiveService = new ImageToTextInterpreter
            {
                ImageFilePath = ImageFilePath,
                SubscriptionKey = this.SubstcriptionKey
            };

            return await cognitiveService.ConvertImageToStreamAndExtractText();
        }

        private string GetSubscriptionKey()
        {
            return ConfigurationManager.AppSettings["DocArcVisionKey"].ToString();
        }
    }
}