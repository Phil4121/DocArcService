using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        public ImageHelper.Format ImageFormat { get; set; } = ImageHelper.Format.No_Change;

        const string UNKNOWN_LANGUAGE = "unk";

        public async Task<OcrResults> ConvertImageToStreamAndExtractText()
        {
            try
            {
                var visionServiceClient = new VisionServiceClient(SubscriptionKey);

                Image newImg = null;

                using (Image img = Image.FromFile(ImageFilePath))
                {
                    var imgHelper = new ImageHelper();
                    newImg = imgHelper.Resize(img, ImageHelper.Format.A4);
                }

                try
                {
                    using (var stream = File.OpenRead(ImageFilePath))
                    {
                        newImg.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        var res = await visionServiceClient.RecognizeTextAsync(stream, UNKNOWN_LANGUAGE);

                        return res;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new OcrResults();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OcrResults();
            }
        }

        public string GetTextFromOCRResult(OcrResults ocr)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (ocr != null && ocr.Regions != null)
            {
                foreach (var item in ocr.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}