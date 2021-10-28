using System;
using System.Data.SqlClient;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public static class DAL
    {
        private static SqlConnection conn = null;

        public static SqlConnection GetConnection()
        {
            //if (conn != null)
            //{
            //    Logger.Log("Reusing current SqlConnection");
            //    return conn;
            //}
            //else
            //{
                string dbServerName = @"localhost\SQLEXPRESS";
                string dbName = "ITStock";
                //string dbUsername = "";
                //string dbPassword = "";

                string connString = $"Data Source = {dbServerName}; Initial Catalog = {dbName}; Integrated Security = True";

                try
                {
                    Logger.Log("Creating new SqlConnection");
                    conn = new SqlConnection(connString);
                    conn.InfoMessage += Conn_InfoMessage;

                    Logger.Log("Opening the SqlConnection");
                    conn.Open();
                    return conn;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    return null;
                }
            //}
        }

        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Logger.Log(e.Message);
        }
    }
}