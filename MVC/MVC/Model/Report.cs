using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model
{
    public abstract class Report
    {
        public abstract void GenerateReport(string text);
    }
}
