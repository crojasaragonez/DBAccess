using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBAccess
{
    public class SqlServerAccess : DBAccess
    {
        private SqlTransaction transaction;
        private SqlConnection connection;
        public SqlServerAccess(string connectionString) : base(connectionString)
        {
            SqlConnectionStringBuilder connectionstring = new SqlConnectionStringBuilder(connectionString);
            try
            {
                this.connection = new SqlConnection(connectionstring.ConnectionString);
            }
            catch (SqlException e)
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
            catch (SqlException e)
            {
                this.ProcessException(e);
            }
        }
        public override void Disconnect()
        {
            try
            {
                connection.Close();
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
                SqlCommand sqlC = this.AddParameters(sql, parameters);
                data.Load(sqlC.ExecuteReader());
            }
            catch (SqlException e)
            {
                ProcessException(e);
            }
            return data;
        }
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            object result = null;
            try
            {
                SqlCommand sqlC = this.AddParameters(sql, parameters);
                result = sqlC.ExecuteScalar();
            }
            catch (SqlException e)
            {
                this.ProcessException(e);
            }
            return result;
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
            SqlCommand cmd = new SqlCommand(sql, connection);
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
    }
}

