using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace DBAccess
{
    public class SqlServerAccessODBC : DBAccess
    {
        private OdbcTransaction transaction;
        private OdbcConnection cn;
        private bool inTransaction;

        public SqlServerAccessODBC(string connectionString) : base(connectionString)
        {
            this.inTransaction = false;
            cn = new OdbcConnection();
            cn.ConnectionString = connectionString;
        }
        public override void Connect()
        {

            if (this.cn.State == ConnectionState.Open) return;
            try
            {
    this.CleanStatus();
    cn.Open();
    
                }
            catch (OdbcException e)
            {
    ProcessException(e);
                }

        }

        public override void Disconnect()
        {
            try
           {
                cn.Close();
            }
            catch (OdbcException e)
            {
                ProcessException(e);
            }

        }

        public override DataTable SqlQuery(string sql, IDictionary<string, object> parameters)
        {
            DataTable data = new DataTable();

            try
            {
                this.CleanStatus();
                OdbcCommand sqlC = AddParameters(sql, parameters);

                data.Load(sqlC.ExecuteReader());
            }
            catch (OdbcException e)
            {
                ProcessException(e);
            }
            return data;
        }

        public override void SqlStatement(string pSql, IDictionary<string, object> parameters)
        {
            try
           {
                this.CleanStatus();
                OdbcCommand sqlC = this.AddParameters(pSql, parameters);
                sqlC.ExecuteNonQuery();
            }
            catch (OdbcException e)
            {
                ProcessException(e);
            }
        }

        private OdbcCommand AddParameters(string sql, IDictionary<string, Object> parameters)
        {
            OdbcCommand cmd = new OdbcCommand(sql, cn);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            if (this.inTransaction)
            {
                cmd.Transaction = this.transaction;
            }
            return cmd;
        }

        public override void BeginTransaction()
        {
            if (!inTransaction)
            {
                this.transaction = this.cn.BeginTransaction();
                this.inTransaction = true;
            }
        }

        public override void RollbackTransaction()
        {
            if (this.inTransaction)
            {
                this.transaction.Rollback();
                this.inTransaction = false;
            }
        }

        public override void CommitTransaction()
        {
            if (this.inTransaction)
            {
                this.transaction.Commit();
                this.inTransaction = false;
            }
        }
    }
}


