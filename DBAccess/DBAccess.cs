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
        protected string connectionString;
        protected bool inTransaction;
        public DBAccess(string connectionString)
        {
            this.connectionString = connectionString;
            this.inTransaction = false;
        }
        public abstract void Connect();
        public abstract void Disconnect();
        public abstract DataTable SqlQuery(string sql, IDictionary<string, Object> parameters);
        public abstract object SqlScalar(string sql, IDictionary<string, Object> parameters);
        public abstract void SqlStatement(string sql, IDictionary<string, Object> parameters);
        public abstract void BeginTransaction();
        public abstract void RollbackTransaction();
        public abstract void CommitTransaction();
        public abstract void NoticeProcedure();
    }
}
