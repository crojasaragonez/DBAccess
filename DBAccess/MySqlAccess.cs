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
        public MySqlAccess(string connectionString) : base(connectionString)
        {
            connection = new MySqlConnection(connectionString);
            this.Connect();
        }
        public override void Connect()
        {
            if (this.connection.State == ConnectionState.Open) return;
            try
            {
                connection.Open();
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public override void Disconnect()
        {
            try
            {
                connection.Close();
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
