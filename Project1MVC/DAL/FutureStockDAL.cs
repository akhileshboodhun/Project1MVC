using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public class FutureStockDAL : IManageDAL<FutureStock>
    {

        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        public bool Add(FutureStock obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FutureStock obj)
        {
            throw new NotImplementedException();
        }

        public FutureStock Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FutureStock> GetAll()
        {
            List<FutureStock> FutureStocksList = new List<FutureStock>();

            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("SELECT * FROM FutureStockView", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var futureStock = new FutureStock();
                    futureStock.Brand = reader["Brand"].ToString();
                    futureStock.Model = reader["Model"].ToString();
                    futureStock.OrderDate = Convert.ToDateTime(reader["OrderDate"].ToString());
                    futureStock.IsOrderComplete = Convert.ToBoolean(reader["IsOrderComplete"].ToString());
                    futureStock.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                    futureStock.Qty = Convert.ToInt32(reader["NetPrice"]);
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

        public bool Update(FutureStock obj)
        {
            throw new NotImplementedException();
        }
    }
}