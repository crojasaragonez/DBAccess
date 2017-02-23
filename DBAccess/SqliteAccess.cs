using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    class SqliteAccess : DBAccess
    {
        // Conection
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
        // conectarse
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
        //Desconectarse
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

        public override DataTable SqlQuery(string sql)
        {
            this.CleanStatus();
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

        public override void SqlStatement(string pSql)
        {
            this.CleanStatus();
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(pSql, this.connection);
                cmd.ExecuteNonQuery();

            }
            catch (SQLiteException e)
            {
                this.ProcessException(e);
            }
        }
    }
}