using System;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

namespace DBAccess
{
   public class MsAccess : DBAccess
    {
        private DataSet set;
        private OleDbConnection con;
        public MsAccess(string connectionString) : base(connectionString)
        {
            set = new DataSet();
            try
            {
                this.con = new OleDbConnection(connectionString);
            }
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception ex)
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
                OleDbCommand myCmd = new OleDbCommand(sql, con);
                foreach (var item in parameters)
                {
                    myCmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                OleDbDataAdapter dat = new OleDbDataAdapter(myCmd);
                dat.SelectCommand = myCmd;
                dat.Fill(retorno);
            }
            catch (Exception ex)
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
                OleDbCommand CmdSql = new OleDbCommand(pSql, con);
                foreach (var item in parameters)
                {
                    CmdSql.Parameters.AddWithValue(item.Key, item.Value);
                }
             
                CmdSql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.ProcessException(ex);
            }

        }
    }
}
