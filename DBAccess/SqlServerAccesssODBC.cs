using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace DBAccess
{
    public class SqlServerAccesssODBC : DBAccess
    {

        private OdbcConnection cn;

        public SqlServerAccesssODBC(string connectionString) : base(connectionString)
        {
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
            return cmd;
        }
    }
}

