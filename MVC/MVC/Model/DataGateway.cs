using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class DataGateway
    {
        protected string path;

        public DataGateway(string path)
        {
            this.path = path;
        }

    }
}
