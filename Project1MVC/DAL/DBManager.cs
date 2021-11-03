using System;
using System.Data.SqlClient;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public class DBManager : IDBProvider
    {
        private DBManager() { }

        public static DBManager Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly DBManager instance = new DBManager();
        }

        public string ServerName
        {
            get
            {
                return @"localhost\SQLEXPRESS";
            }
        }

        public string DatabaseName
        {
            get
            {
                return "ITStock";
            }
        }

        public string ConnectionString
        {
            get
            {
                return $"Data Source = {this.ServerName}; Initial Catalog = {this.DatabaseName}; Integrated Security = True";
            }
        }

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(this.ConnectionString);
                //conn.InfoMessage += Conn_InfoMessage;

                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return null;
            }
        }

        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            //Logger.Log(e.Message);
        }
    }
}