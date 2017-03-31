using System;

namespace DBAccess
{
    public class ErrorHandler 
    {
        public bool isError { get; set; }
        public string errorDescription { get; set; }

        protected void ProcessException(Exception e) {
            this.isError = true;
            this.errorDescription = e.Message;
        }

        protected void ProcessStoreProcedureException(string message)
        {
            this.isError = true;
            this.errorDescription = message;
        }

        protected void CleanStatus()
        {
            this.isError = false;
            this.errorDescription = string.Empty;
        }
    }
}
