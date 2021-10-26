using Project1MVC.Models;
using Project1MVC.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public class FutureStockDAL
    {


        /*private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection()"].ToString();
            con = new SqlConnection(constring);
        }*/

        private SqlConnection con { get; set; }

        public FutureStockDAL()
        {
            con = new DBConnection().GetConnection();
        }

        public IEnumerable<FutureStock> GetAll()
        {
            List<FutureStock> FutureStocksList = new List<FutureStock>();
            var con = new DBConnection().GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM FutureStockView", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var futureStock = new FutureStock();
                    futureStock.Id = Convert.ToInt32(reader["Id"]);
                    futureStock.Brand = reader["Brand"].ToString();
                    futureStock.Model = reader["Model"].ToString();
                    futureStock.OrderDate = Convert.ToDateTime(reader["OrderDate"].ToString());
                    futureStock.IsOrderComplete = Convert.ToBoolean(reader["IsOrderComplete"].ToString());
                    futureStock.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                    futureStock.Qty = Convert.ToInt32(reader["Qty"]);
                    futureStock.NetPrice = Convert.ToDouble(reader["NetPrice"]);

                    futureStock.SupplierName = reader["SupplierName"].ToString();
                    FutureStocksList.Add(futureStock);
                }

                return FutureStocksList;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + e.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
}