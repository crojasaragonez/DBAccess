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
            try
            {
                this.connection = new NpgsqlConnection(conectionstring.ConnectionString);
            }
            catch (NpgsqlException e)
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

        public override DataTable SqlQuery(string sql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            NpgsqlCommand cmd = this.AddParameters(sql, parameters);

            NpgsqlDataAdapter oDataAdapter = new NpgsqlDataAdapter(cmd);
            DataTable result = new DataTable();
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

        public override void SqlStatement(string sql, IDictionary<string, Object> parameters)
        {
            this.CleanStatus();
            try
            {
                NpgsqlCommand cmd = this.AddParameters(sql, parameters);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                this.ProcessException(e);
            }
        }

        private NpgsqlCommand AddParameters(string sql, IDictionary<string, Object> parameters)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sql, this.connection);
            cmd.CommandType = CommandType.Text;
            foreach (var parameter in parameters)
            {
                NpgsqlParameter p = new NpgsqlParameter(parameter.Key, parameter.Value);
                p.DbType = DBTypeResolver.Resolve(parameter.Value.GetType());
                cmd.Parameters.Add(p);
            }
            return cmd;
        }
    }
}
