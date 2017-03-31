﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DBAccess
{
    public class PgAccess : DBAccess
    {
        private NpgsqlConnection connection;
        private NpgsqlTransaction transaction;
        public PgAccess(string connectionString) : base(connectionString)
        {
            try
            {
                this.connection = new NpgsqlConnection(this.connectionString);             
            }
            catch (NpgsqlException e)
            {
                this.ProcessException(e);
            }
            this.Connect();

            this.connection.Notice += new NoticeEventHandler((object sender, NpgsqlNoticeEventArgs e) => {
                ProcessStoreProcedureException(e.Notice.MessageText);
            });
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
        public override object SqlScalar(string sql, IDictionary<string, object> parameters)
        {
            this.CleanStatus();        
            object result = null;
            try
            {
                NpgsqlCommand cmd = this.AddParameters(sql, parameters);
                result = cmd.ExecuteScalar();               
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
                cmd.Parameters.AddWithValue(parameter.Key, (parameter.Value) ?? DBNull.Value);
            }
            if (this.inTransaction) {
                cmd.Transaction = this.transaction;
            }
            return cmd;
        }
        public override void BeginTransaction()
        {
            if (!inTransaction) {
                this.transaction = this.connection.BeginTransaction();
                this.inTransaction = true;
            }
        }
        public override void RollbackTransaction()
        {
            if (this.inTransaction) {
                this.transaction.Rollback();
                this.inTransaction = false;
            }
        }
        public override void CommitTransaction()
        {
            if (this.inTransaction) {
                this.transaction.Commit();
                this.inTransaction = false;
            }
        }
    }
}
