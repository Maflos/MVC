using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model
{
    public class ReportPDF : Report
    {
        public override void GenerateReport(string text)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("report.pdf", FileMode.Create));
            doc.Open();

            Paragraph paragraph = new Paragraph(text);
            doc.Add(paragraph);
            doc.Close();
        }
    }
}
