using System;
using System.Data;
using System.Data.SqlClient;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public sealed class DBManager : IDBProvider, IDisposable
    {
        //private DBManager() { }

        //public static DBManager Instance { get { return Nested.instance; } }

        //private class Nested
        //{
        //    // Explicit static constructor to tell C# compiler not to lazily instantiate us
        //    static Nested() { }

        //    internal static readonly DBManager instance = new DBManager();
        //}

        private readonly SqlConnection conn = null;
        private bool disposedValue;

        public DBManager()
        {
            try
            {
                conn = new SqlConnection(this.ConnectionString);
                conn.InfoMessage += Conn_InfoMessage;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
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

        public SqlConnection GetConnection
        {
            get
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                return conn;
            }
        }

        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            //Logger.Log(e.Message);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    conn.Close();
                    conn.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}