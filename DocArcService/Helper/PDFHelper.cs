using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DocArcService.Helper
{
    public class PDFHelper
    {
        public string SaveImageAsPdf(string imageFileName, string pdfFileName, int height = 1024, int width = 786, bool deleteImage = false)
        {
            using (var document = new PdfDocument())
            {
                try
                {
                    PdfPage page = document.AddPage();
                    using (XImage img = XImage.FromFile(imageFileName))
                    {
                        // Change PDF Page size to match image
                        page.Width = width;
                        page.Height = height;

                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        gfx.DrawImage(img, 0, 0, width, height);
                    }
                    document.Save(pdfFileName);

                    if (deleteImage)
                        File.Delete(imageFileName);


                    return pdfFileName;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return string.Empty;
                }
            }
        }
    }
}