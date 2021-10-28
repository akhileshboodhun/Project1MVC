using System;
using System.Data.SqlClient;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public static class DBManager
    {
        public static SqlConnection GetConnection()
        {
            string dbServerName = @"localhost\SQLEXPRESS";
            string dbName = "ITStock";
            //string dbUsername = "";
            //string dbPassword = "";

            string connString = $"Data Source = {dbServerName}; Initial Catalog = {dbName}; Integrated Security = True";

            try
            {
                SqlConnection conn = new SqlConnection(connString);
                //conn.InfoMessage += Conn_InfoMessage;
                //conn.StateChange += Conn_StateChange;
                Logger.Log("SqlConnection created");

                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                //Logger.Flush();
                return null;
            }
        }

        private static void Conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            //if (e.CurrentState.ToString().Contains("close"))
            //{
            //    Logger.Log("SqlConnection closed" + Environment.NewLine);
            //}
            //else if (e.CurrentState.ToString().Contains("open"))
            //{
            //    Logger.Log("SqlConnection opened");
            //}

            //Logger.Flush();
        }

        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            //Logger.Log(e.Message);
        }
    }
}