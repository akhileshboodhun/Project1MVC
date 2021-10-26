using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.Utils
{
    public class DBConnection
    {
        public SqlConnection GetConnection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(constring);
                return new SqlConnection(constring);
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }
    }
}