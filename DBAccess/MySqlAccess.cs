using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace DBAccess
{
    public class MySqlAccess : DBAccess
    {
        private MySqlConnection connection;
        private MySqlTransaction transaction;
        public MySqlAccess(string connectionString) : base(connectionString)
        {
            try
            {
                this.connection = new MySqlConnection(this.connectionString);
            }
            catch (MySqlException e)
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
                this.connection.Open();
            }
            catch (MySqlException e)
            {
                this.ProcessException(e);
            }
        }
        public override void Disconnect()
        {
            try
            {
                this.connection.Close();
            }
            catch (MySqlException ex)
            {
                this.ProcessException(ex);
            }
        }
        public override DataTable SqlQuery(string sql, IDictionary<string, object> parameters)
        {
            MySqlCommand cmd = this.AddParameters(sql, parameters);
            MySqlDataAdapter oDataAdapter = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            DataTable result = new DataTable();
            result.Locale = CultureInfo.InvariantCulture;
            try
            {
                oDataAdapter.Fill(result);
            }
            catch (MySqlException ex)
            {
                this.ProcessException(ex);
            }
            return result;
        }
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            object result = null;
            try
            {
                MySqlCommand cmd = this.AddParameters(sql, parameters);
                result = cmd.ExecuteScalar();
            }
            catch (MySqlException e)
            {
                this.ProcessException(e);
            }
            return result;
        }
        public override void SqlStatement(string pSql, IDictionary<string, object> parameters)
        {
            try
            {
                MySqlCommand cmd = this.AddParameters(pSql, parameters);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                this.ProcessException(ex);
            }
        }
        private MySqlCommand AddParameters(string sql, IDictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(sql, this.connection);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, (parameter.Value) ?? DBNull.Value);
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
