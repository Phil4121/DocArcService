using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Helper
{
    public class ImageHelper : IDisposable
    {
        public enum Format { No_Change, A4 };

        public string Resize(string imageFilePath, Format format)
        {
            try
            {
                var tmpFilePath = imageFilePath + ".tmp";

                using (var srcImage = Image.FromFile(imageFilePath))
                {
                    var newWidth = 0;
                    var newHeight = 0;

                    if (format == Format.A4)
                    {
                        newWidth = 786;
                        newHeight = 1024;
                    }

                    using (var newImage = new Bitmap(newWidth, newHeight))
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));

                        newImage.Save(tmpFilePath,ImageFormat.Jpeg);
                        newImage.Dispose();
                    }
                }


                if (System.IO.File.Exists(tmpFilePath))
                {
                    System.IO.File.Delete(imageFilePath);
                    System.IO.File.Move(tmpFilePath, imageFilePath);
                    System.IO.File.Delete(tmpFilePath);
                }
                else
                {
                    throw new Exception("TMP File not exists!");
                }

                return imageFilePath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public void Dispose()
        {
            
        }
    }
}