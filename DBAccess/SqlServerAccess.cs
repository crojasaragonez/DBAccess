using System;
using System.Data;
using System.Data.SqlClient;

namespace DBAccess
{
    public class SqlServerAccess : DBAccess
    {

        private String connectionString;
        private SqlConnection conn;

        public SqlServerAccess(string connectionString) : base(connectionString)
        {
            this.connectionString = connectionString;
        }
        public override void Connect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = this.ConnectionString();
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

        public override DataTable SqlQuery(string sql)
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

        public override void SqlStatement(string pSql)
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


        private String ConnectionString()
        {
            String[] datos = connectionString.Split(',');

            return String.Format("data source={0};initial catalog={1};user id={2};password={3}",
                    datos[0],
                    datos[1],
                    datos[2],
                    datos[3]
            );
        }
    }
}
