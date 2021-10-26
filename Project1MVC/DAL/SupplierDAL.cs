using Project1MVC.Models;
using Project1MVC.Services;
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
    public class SupplierDAL : IManageDAL<Supplier>
    {
        /* private SqlConnection con;
         private void connection()
         {
             string constring = ConfigurationManager.ConnectionStrings["ItStocknew DBConnection()"].ToString();
             con = new SqlConnection(constring);
         }*/
        private SqlConnection con { get; set; }

        public SupplierDAL()
        {
            con = new DBConnection().GetConnection();
        }

        // **************** ADD NEW SUPPLIER *********************
        public bool Add(Supplier supplier)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@PhoneNo", supplier.PhoneNo);
                cmd.Parameters.AddWithValue("@Address", supplier.Address);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                return (i >= 1);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            
        }


        public Supplier Get(int supplierId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                var fields = dt.Rows[0];
                return new Supplier(Convert.ToInt32(fields["SupplierId"]), fields["Name"].ToString(), fields["PhoneNo"].ToString(), fields["Address"].ToString());

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            List<Supplier> SupplierList = new List<Supplier>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllSuppliers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    SupplierList.Add(

                        new Supplier(Convert.ToInt32(dr["SupplierId"]), dr["Name"].ToString(), dr["PhoneNo"].ToString(), dr["Address"].ToString())
                        );
                }

                return SupplierList;
            }

            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            } 
        }

        public bool Update(Supplier supplier)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@PhoneNo", supplier.PhoneNo);
                cmd.Parameters.AddWithValue("@Address", supplier.Address);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return (i >= 1);
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                con.Close();
            }

           
        }

        public bool Delete(Supplier supplier)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteSupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                return (i >= 1);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
           
        }
    }
}