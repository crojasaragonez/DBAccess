using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;

namespace DBAccess
{
   public class MsAccess : DBAccess
    {
        private OleDbConnection connection;
        private OleDbTransaction transaction;
        public MsAccess(string connectionString) : base(connectionString)
        {
            try
            {
                this.connection = new OleDbConnection(this.connectionString);
            }
            catch (OleDbException e)
            {
                this.ProcessException(e);
            }
            this.Connect();

            this.connection.InfoMessage += new OleDbInfoMessageEventHandler((object sender, OleDbInfoMessageEventArgs e) => {
                ProcessStoreProcedureException(e.Message);
            });
        }
        public override void Connect()
        {
            this.CleanStatus();
            if (this.connection.State == ConnectionState.Open) return;
            try
            {
                this.connection.Open();
            }
            catch (OleDbException e)
            {
                this.ProcessException(e);
            }    
        }
        public override void Disconnect()
        {
            this.CleanStatus();
            try
            {
                this.connection.Close();
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            }      
        }
        public override DataTable SqlQuery(string sql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            DataTable retorno = null;
            try
            {
                OleDbCommand CmdSql = this.AddParameters(sql, parameters);
                OleDbDataAdapter dat = new OleDbDataAdapter(CmdSql);
                dat.SelectCommand = CmdSql;
                dat.Fill(retorno);
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            } 
            return retorno;
        }
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            object result = null;
            try
            {
                OleDbCommand CmdSql = this.AddParameters(sql, parameters);
                result = CmdSql.ExecuteScalar();
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            }
            return result;
        }
        public override void SqlStatement(string sql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            try
            {
                OleDbCommand CmdSql = this.AddParameters(sql, parameters);
                CmdSql.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            }
        }
        private OleDbCommand AddParameters(string sql, IDictionary<string, Object> parameters)
        {
            OleDbCommand cmd = new OleDbCommand(sql, this.connection);
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
            if (inTransaction)
            {
                this.transaction.Rollback();
                this.inTransaction = false;
            }
        }
        public override void CommitTransaction()
        {
            if (inTransaction)
            {
                this.transaction.Commit();
                this.inTransaction = false;
            }
        }
    }
}
