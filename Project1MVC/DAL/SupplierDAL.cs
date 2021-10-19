using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project1MVC.DAL
{
    public class SupplierDAL
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW SUPPLIER *********************
        public bool AddStudent(Supplier supplier)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewSupplier", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", supplier.Name);
            cmd.Parameters.AddWithValue("@City", supplier.City);
            cmd.Parameters.AddWithValue("@Address", supplier.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}