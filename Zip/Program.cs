using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Zip
{
    class Program
    {
        static void Main(string[] args)
        {
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            string fileName = "export_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
            byte[] fileBytes = uniEncoding.GetBytes("Invalid file path characters are: ");
            byte[] compressedBytes;
            string fileNameZip = "Export_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".zip";
            var outStream = new MemoryStream();
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Update, true))
                {
                    /*
                    var fileInArchive = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(fileBytes))
                    {
                        StreamWriter streamWriter = new StreamWriter(archive.);
                        fileToCompressStream.CopyTo(entryStream);
                    }*/
                    for (int i = 0; i < 5; i++)
                    {
                        var demoFile = archive.CreateEntry("file"+i+".txt");
                        using (var entryStream = demoFile.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write("test! " +i);
                        }
                    }
                    //archive.ExtractToDirectory("c:/temp");        
                }
            FileStream fs = new FileStream(@"C:\Temp\test.zip", FileMode.Create);
                
                    //outStream.Position = 0;
                    //outStream.CopyTo(fs);
                
               // compressedBytes = outStream.ToArray();
                //outStream.Flush();
            //outStream.Position = 0;     // read from the start of what was written
            string from = "rnd@algomizer.com";
            var fromAddress = new MailAddress(from, "guy");
            MailMessage message = new MailMessage(
               "guy@algomizer.com",
               "guy@algomizer.com",
               "Quarterly data report.",
               "See the attached spreadsheet.");

                message = new MailMessage();
                message.Subject = "zip";
                message.Body = "body";
//                message.IsBodyHtml = true;
                message.From = fromAddress;
                message.To.Add("guy@algomizer.com");
//                message.BodyEncoding = Encoding.UTF8;
//                message.HeadersEncoding = Encoding.UTF8;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Algo2014Algo")

            };
            //System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Zip);
            //System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(outStream,fileNameZip, ct.MediaType);
            MemoryStream attachmentStream = new MemoryStream(outStream.ToArray());
            outStream.Position = 0;
            Attachment attachment = new Attachment(outStream, fileNameZip, MediaTypeNames.Application.Zip);
            message.Attachments.Add(attachment);
            
            smtp.Send(message);
//            SmtpClient client = new SmtpClient(server);
            // Add credentials if the SMTP server requires them.


        }
    }
}

