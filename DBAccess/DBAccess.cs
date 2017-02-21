using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public abstract class DBAccess : ErrorHandler
    {
        string connectionString;
        public DBAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public abstract void Connect();
        public abstract void Disconnect();
        public abstract DataTable SqlQuery(string sql);
        public abstract void SqlStatement(String pSql);
    }
}
