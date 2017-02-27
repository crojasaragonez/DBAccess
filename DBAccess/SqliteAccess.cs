using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace DBAccess
{
    class SqliteAccess : DBAccess
    {
        private SQLiteConnection connection;
        public SqliteAccess(string connectionString) : base(connectionString)
        {
            SQLiteConnectionStringBuilder connectionstring = new SQLiteConnectionStringBuilder(connectionString);
            try
            {
                this.connection = new SQLiteConnection(connectionstring.ConnectionString);
            }
            catch (SQLiteException e)
            {
                this.ProcessException(e);
            }
            this.Connect();


        }
        public override void Connect()
        {
            if (this.connection.State == ConnectionState.Open)
                return;
            try
            {
                connection.Open();
            }
            catch (SQLiteException e)
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
            catch (SQLiteException e)
            {
                this.ProcessException(e);
            }
        }

        public override DataTable SqlQuery(string sql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            SQLiteCommand cmd = this.AddParameters(sql, parameters);

            SQLiteDataAdapter oDataAdapter = new SQLiteDataAdapter(sql, this.connection);
            DataTable result = new DataTable();
            result.Locale = CultureInfo.InvariantCulture;
            try
            {
                oDataAdapter.Fill(result);
            }
            catch (SQLiteException e)
            {
                this.ProcessException(e);
            }

            return result;
        }

        public override void SqlStatement(string pSql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            try
            {
                SQLiteCommand cmd = this.AddParameters(pSql, parameters);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                this.ProcessException(e);
            }
        }
        private SQLiteCommand AddParameters(string sql, IDictionary<string, Object> parameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, this.connection);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            return cmd;
        }
    }
}