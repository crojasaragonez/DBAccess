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
        private SQLiteTransaction transaction;
        public SqliteAccess(string connectionString) : base(connectionString)
        {
            try
            {
                this.connection = new SQLiteConnection(this.connectionString);
            }
            catch (SQLiteException e)
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
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();
            object result = null;
            try
            {
                SQLiteCommand cmd = this.AddParameters(sql, parameters);
                result = cmd.ExecuteScalar();
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
                cmd.Parameters.AddWithValue(parameter.Key, (parameter.Value) ?? DBNull.Value);
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