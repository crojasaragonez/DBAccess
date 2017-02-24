using System;
using System.Data.OleDb;
using System.Data;
using System.Configuration;


namespace DBAccess
{
    class MsAccess : DBAccess
    {
        private DataSet set;
        private OleDbConnection con;
        public MsAccess(string connectionString) : base(connectionString)
        {
            set = new DataSet();
        }
        public override void Connect()
        {
            this.CleanStatus();
            ConnectionStringSettings conSettings;
            try
            {
                conSettings = ConfigurationManager.ConnectionStrings["MyAccessDBConnection"];
                string connectionString = conSettings.ConnectionString;
                con = new OleDbConnection(connectionString);
            }
            catch (Exception ex)
            {
                this.ProcessException(ex);
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

        public override DataTable SqlQuery(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            DataTable retorno = null;
            try
            {
                OleDbDataAdapter dat = new OleDbDataAdapter(sql, con);
                dat.Fill(retorno);
            }
            catch (Exception ex)
            {
                this.ProcessException(ex);
            } 
            return retorno;
        }

        public override void SqlStatement(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            try
            {
                OleDbCommand CmdSql = new OleDbCommand(pSql, con);
                CmdSql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.ProcessException(ex);
            }

        }
    }
}
