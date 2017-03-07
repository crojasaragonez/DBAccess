using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;

namespace DBAccess
{
   public class MsAccess : DBAccess
    {
        private OleDbConnection con;
        private OleDbCommand CmdSql;
        public MsAccess(string connectionString) : base(connectionString)
        {
            try
            {
                this.con = new OleDbConnection(connectionString);
            }
            catch (OleDbException e)
            {
                this.ProcessException(e);
            }
           
            this.Connect();
        }
        public override void Connect()
        {
            this.CleanStatus();
            if (this.con.State == ConnectionState.Open) return;
            try
            {
                con.Open();
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
                con.Close();
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
                CmdSql = new OleDbCommand(sql, con);
                this.addParameters(parameters);
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

        public override void SqlStatement(string pSql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            try
            {
                CmdSql = new OleDbCommand(pSql, con);
                this.addParameters(parameters);
                CmdSql.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            }

        }
        private void addParameters(IDictionary<string, Object> parameters)
        {
            try
            {
                foreach (var item in parameters)
                {
                    this.CmdSql.Parameters.AddWithValue(item.Key, item.Value);
                }
            }
            catch (OleDbException ex)
            {
                this.ProcessException(ex);
            }        
        }

        public override void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public override void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public override void CommitTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
