using DocArcService.AbstractClasses;
using DocArcService.Classes;
using DocArcService.Helper;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Provider
{
    public class CognitiveServiceProvider : FileProcessingProvider
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

        private string GetSubscriptionKey()
        {
            return ConfigurationManager.AppSettings["DocArcVisionKey"].ToString();
        }

        public override async Task<bool> ProzessImage(string blobFileName, string container)
        {
            try
            {
                var fileHelper = new FileHelper();
                var pdfHelper = new PDFHelper();
                var imageHelper = new ImageHelper();

                // 1. check if exists 
                if (! await fileHelper.BlobExists(blobFileName,container))
                    throw new Exception("Blobfile does not exists!");

                // 2. download to filesystem
                var tmpFileLocation = await fileHelper.GetBlobFromAzureStorage(blobFileName, container, AppDomain.CurrentDomain.BaseDirectory);

                // 3. check if valid image file!
                if (!imageHelper.IsValidImage(tmpFileLocation))
                    return false;

                // idear: first pdf -> send pdf to ocr (better for futher use?)

                // 4. process ocr
                var ocr = await ConvertImageToTextAsync(tmpFileLocation);


                // 5. save ocr
                //throw new NotImplementedException();
                //throw new Exception("rebuild for multiple images!!!!");

                // 6. convert to pdf
                tmpFileLocation = pdfHelper.SaveImageAsPdf(tmpFileLocation, tmpFileLocation);

                // 7. upload and replace original
                var uploadOK = await fileHelper.UploadBlobToAzureStorage(blobFileName, tmpFileLocation, container); 

                return uploadOK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally{

                var tmpFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + blobFileName;

                if (File.Exists(tmpFilePath)){ 
                    File.Delete(tmpFilePath);
                }
            }
        }

        private async Task<OcrResults> ConvertImageToTextAsync(string ImageFilePath)
        {
            var cognitiveService = new ImageToTextInterpreter
            {
                ImageFilePath = ImageFilePath,
                SubscriptionKey = this.SubstcriptionKey,
                ImageFormat = ImageHelper.Format.A4
            };

            return await cognitiveService.ConvertImageToStreamAndExtractText();
        }
    }
}