using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBAccess
{
    public class SqlServerAccess : DBAccess
    {


        private SqlConnection conn;

        public SqlServerAccess(string connectionString) : base(connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
        }
        public override void Connect()
        {          
            
            try
            {
                this.CleanStatus();
                conn.Open();

            }
            catch (Exception e)
            {
                ProcessException(e);
            }

        }

        public override void Disconnect()
        {
            try
            {
                conn.Close();
            }
            catch (Exception e)
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
                SqlCommand result = new SqlCommand(sql, conn);
                data.Load(result.ExecuteReader());
            }
            catch (Exception e)
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
                SqlCommand result = new SqlCommand(pSql, conn);
                result.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ProcessException(e);
            }
        }
    }
}
