using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace CostHistory.Models
{
    public static class Connection
    {
        public static DataTable GetData(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SMLConnectionString"].ToString());
            DataTable dt = new DataTable("Order");
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            da.SelectCommand = cmd;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            conn.Close();
            da.Fill(dt);
            return dt;
        }


        public static void ExecuteSqlTransaction(List<string> sqlCommands)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SMLConnectionString"].ToString()))
            {
                connection.Open();

                NpgsqlCommand command = connection.CreateCommand();
                NpgsqlTransaction transaction;
                transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    foreach (var item in sqlCommands)
                    {
                        command.CommandText = item;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        throw ex;

                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
            }
        }
    }
}