using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Auth.Protocol;
using Microsoft.WindowsAzure.Storage.Core.Auth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace DocArcService.Helper
{
    public class ImageHelper
    {
        public enum Format { No_Change, A4 };

        public Image Resize(Image image, Format format)
        {
            try
            {
                var newWidth = 0;
                var newHeight = 0;

                if (format == Format.A4)
                {
                    newWidth = 786;
                    newHeight = 1024;
                }

                var newImage = new Bitmap(newWidth, newHeight);

                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
                }

                return newImage;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool IsValidImage(string imagePath)
        {
            try
            {
                Image.FromFile(imagePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}