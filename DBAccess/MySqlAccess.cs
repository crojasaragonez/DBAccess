using MySql.Data.MySqlClient;
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
                throw ex;
            }
        }

        public override DataTable SqlQuery(string sql)
        {
            MySqlDataAdapter oDataAdapter = new MySqlDataAdapter(sql, this.connection);
            DataTable result = new DataTable();
            result.Locale = CultureInfo.InvariantCulture;
            try
            {
                oDataAdapter.Fill(result);
            }
            catch (MySqlException e)
            {
                throw e;
            }

            return result;
        }

        public override void SqlStatement(string pSql)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(pSql, this.connection);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                throw e;
            }
            
        }
    }
}
