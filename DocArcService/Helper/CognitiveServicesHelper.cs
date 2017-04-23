using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Helper
{
    public class CognitiveServicesHelper
    {
    }

    public class ImageToTextInterpreter
    {
        public string ImageFilePath { get; set; }
 
        public string SubscriptionKey { get; set; }
 
        const string UNKNOWN_LANGUAGE = "unk";
        
        public async Task<OcrResults> ConvertImageToStreamAndExtractText()
        {
            var visionServiceClient = new VisionServiceClient(SubscriptionKey);
 
            using (Stream imageFileStream = File.OpenRead(ImageFilePath))
            {
                return await visionServiceClient.RecognizeTextAsync(imageFileStream, UNKNOWN_LANGUAGE);
            }
        }
    }
}