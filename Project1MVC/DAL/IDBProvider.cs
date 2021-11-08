using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public interface IDBProvider
    {
        string ServerName { get; }

        string DatabaseName { get; }

        string ConnectionString { get; }

        SqlConnection GetConnection { get; }
    }
}