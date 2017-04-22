using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model
{
    public class ReportFactory
    {
        public Report GetReport(string reportType)
        {
            if(reportType == null)
            {
                return null;
            }
            else if (reportType == "pdf")
            {
                return new ReportPDF();
            }
            else if (reportType == "csv")
            {
                return new ReportCSV();
            }

            return null;
        }

   }
}
