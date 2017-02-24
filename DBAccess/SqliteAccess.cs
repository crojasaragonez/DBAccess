using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    class SqliteAccess : DBAccess
    {
        public SqliteAccess(string connectionString) : base(connectionString)
        {

        }
        public override void Connect()
        {
            throw new NotImplementedException();
        }

        public override void Disconnect()
        {
            throw new NotImplementedException();
        }

        public override DataTable SqlQuery(string sql, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SqlStatement(string pSql, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
