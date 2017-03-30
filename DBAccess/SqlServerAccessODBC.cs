using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace DBAccess
{
    public class SqlServerAccessODBC : DBAccess
    {
        private OdbcTransaction transaction;
        private OdbcConnection connection;
        public SqlServerAccessODBC(string connectionString) : base(connectionString)
        {
            try
            {
                this.connection = new OdbcConnection(this.connectionString);
            }
            catch (OdbcException e)
            {
                this.ProcessException(e);
            }
            this.Connect();
        }
        public override void Connect()
        {
            if (this.connection.State == ConnectionState.Open) return;
            try
            {
                this.CleanStatus();
                this.connection.Open();
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
                connection.Close();
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
            this.CleanStatus();
            this.CleanProcedureMessage();
            try
            {
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
            OdbcCommand cmd = new OdbcCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, (parameter.Value) ?? DBNull.Value);
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
                this.transaction = this.connection.BeginTransaction();
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
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            this.CleanProcedureMessage();
            object result = null;
            try
            {
                OdbcCommand sqlC = this.AddParameters(sql, parameters);
                result = sqlC.ExecuteScalar();
            }
            catch (OdbcException e)
            {
                this.ProcessException(e);
            }
            return result;
        }

        public override void NoticeProcedure()
        {
            this.connection.InfoMessage += new OdbcInfoMessageEventHandler((object sender, OdbcInfoMessageEventArgs e) => {
                Procedure(e.Message);
            });
        }
    }
}



