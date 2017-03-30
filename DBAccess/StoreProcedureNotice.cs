using System.Linq;
namespace DBAccess
{
    public class StoreProcedureNotice
    {
        public bool isProcedureMessage { get; set; }
        public string messageProcedureDescription { get; set; }
        
        protected void Procedure(string message)
        {          
            this.isProcedureMessage = true;
            this.messageProcedureDescription = message;         
        }

        protected void CleanProcedureMessage()
        {
            this.isProcedureMessage = false;
            this.messageProcedureDescription = null;
        }
    }
}
