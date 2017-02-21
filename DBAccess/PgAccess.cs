using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class PgAccess : DBAccess
    {
        private NpgsqlConnection connection;
        public PgAccess(string connectionString) : base(connectionString)
        {
            NpgsqlConnectionStringBuilder conectionstring = new NpgsqlConnectionStringBuilder(connectionString);
            this.connection = new NpgsqlConnection(conectionstring.ConnectionString);
        }

        public override void Connect()
        {
            if (this.connection.State == ConnectionState.Open) return;
            try
            {
                connection.Open();
            }
            catch (NpgsqlException e)
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
            catch (NpgsqlException e)
            {
                this.ProcessException(e);
            }
        }

        public override DataTable SqlQuery(string sql)
        {
            this.CleanStatus();
            NpgsqlDataAdapter oDataAdapter = new NpgsqlDataAdapter(sql, this.connection);
            DataTable result = new DataTable();
            result.Locale = CultureInfo.InvariantCulture;
            try
            {
                oDataAdapter.Fill(result);
            }
            catch (NpgsqlException e)
            {
                this.ProcessException(e);
            }

            return result;
        }

        public override void SqlStatement(string sql)
        {
            this.CleanStatus();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(sql, this.connection);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                this.ProcessException(e);
            }
        }
    }
}
