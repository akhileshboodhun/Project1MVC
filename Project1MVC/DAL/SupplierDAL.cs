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
    public class SupplierDAL : IManageDAL<Supplier>
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW SUPPLIER *********************
        public bool Add(Supplier supplier)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddSupplier", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", supplier.Name);
            cmd.Parameters.AddWithValue("@PhoneNo", supplier.PhoneNo);
            cmd.Parameters.AddWithValue("@Address", supplier.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }


        public Supplier Get(int supplierId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("GetSupplier", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierId", supplierId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            var fields = dt.Rows[0];
            return new Supplier(Convert.ToInt32(fields["SupplierId"]), fields["Name"].ToString(), fields["PhoneNo"].ToString(), fields["Address"].ToString());
        }

        public IEnumerable<Supplier> GetAll()
        {
            connection();
            List<Supplier> SupplierList = new List<Supplier>();


            SqlCommand cmd = new SqlCommand("GetAllSuppliers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            
            foreach (DataRow dr in dt.Rows)
            {

                SupplierList.Add(

                    new Supplier(Convert.ToInt32(dr["SupplierId"]), dr["Name"].ToString(), dr["PhoneNo"].ToString(), dr["Address"].ToString())
                    );
            }

            return SupplierList;
        }

        public bool Update(Supplier supplier)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateSupplier", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
            cmd.Parameters.AddWithValue("@Name", supplier.Name);
            cmd.Parameters.AddWithValue("@PhoneNo", supplier.PhoneNo);
            cmd.Parameters.AddWithValue("@Address", supplier.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public bool Delete(Supplier supplier)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteSupplier", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }
    }
}