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

            if (this.conn.State == ConnectionState.Open) return;
            try
            {
                this.CleanStatus();
                conn.Open();

            }
            catch (SqlException e)
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
            catch (SqlException e)
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
                SqlCommand sqlC = AddParameters(sql, parameters);

                data.Load(sqlC.ExecuteReader());
            }
            catch (SqlException e)
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
                SqlCommand sqlC = this.AddParameters(pSql, parameters);    
                sqlC.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                ProcessException(e);
            }
        }

        private SqlCommand AddParameters(string sql, IDictionary<string, Object> parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            return cmd;
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

