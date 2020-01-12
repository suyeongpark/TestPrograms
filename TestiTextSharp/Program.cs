using System;
using System.Diagnostics;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.LGPLv2.Core.System.Encodings;
using iTextSharp.LGPLv2.Core.System.NetUtils;

namespace TestiTextSharp
{
    class Program
    {
        const string PATH_PDF = @"C:\Users\suyeo\Downloads\1, 3, 6, 9.pdf";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PdfReader reader = new PdfReader(PATH_PDF);



                for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                {
                    byte[] rawBytes = reader.GetPageContent(pageNo);
                    byte[] utf8Bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, rawBytes);
                    string pageText = Encoding.UTF8.GetString(utf8Bytes);

                    Console.WriteLine(pageText);
                }

            reader.Close();
        }
    }
}
