﻿using System;

namespace DBAccess
{
    public class ErrorHandler : StoreProcedureNotice
    {
        public bool isError { get; set; }
        public string errorDescription { get; set; }

        protected void ProcessException(Exception e) {
            this.isError = true;
            this.errorDescription = e.Message;
        }

        protected void CleanStatus()
        {
            this.isError = false;
            this.errorDescription = string.Empty;
        }
    }
}
