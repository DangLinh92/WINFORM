using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.TeamFoundation.WorkItemTracking.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sMail
{
    public static class PDFGenerate
    {
        public static byte[] GeneratePdf(string message, string ImagePath)
        {
            StringReader sr = new StringReader(message);
            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                return bytes;
            }
        }

        public static byte[] encode(string stringToEncode)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] bytename = new byte[1024];
            bytename = utf8.GetBytes(stringToEncode);
            return bytename;
        }

        public static MemoryStream GeneratePdf1(string message, string ImagePath)
        {
            MemoryStream output = new MemoryStream();
            Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, output);
            pdfDoc.Open();
            Paragraph Text = new Paragraph(message);
            pdfDoc.Add(Text);
            byte[] file;
            file = System.IO.File.ReadAllBytes(ImagePath);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(file);
            jpg.ScaleToFit(550F, 200F);
            pdfDoc.Add(jpg);
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            output.Position = 0;
            return output;
        }
    }
}
