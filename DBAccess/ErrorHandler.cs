using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class ErrorHandler
    {
        public bool isError { get; set; }
        public string errorDescription { get; set; }
       public void ProcessException(Exception e)
        {
            this.isError = true;
            this.errorDescription = e.Message;
        }

        public void CleanStatus()
        {
            this.isError = false;
            this.errorDescription = string.Empty;
        }
    }
}
