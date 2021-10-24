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
    public class EquipmentDAL : IManageDAL<Equipment>
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        public bool Add(Equipment equipment)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddEquipment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Brand", equipment.Brand);
            cmd.Parameters.AddWithValue("@Model", equipment.Model);
            cmd.Parameters.AddWithValue("@Description", equipment.Description);
            cmd.Parameters.AddWithValue("@CurrentStockCount", equipment.CurrentStockCount);
            cmd.Parameters.AddWithValue("@ReStockThreshold", equipment.ReStockThreshold);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public bool Delete(Equipment equipment)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteEquipment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipId", equipment.EquipId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public Equipment Get(int equipId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("GetEquipment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipId", equipId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            var fields = dt.Rows[0];
            return new Equipment(Convert.ToInt32(fields["EquipId"]), fields["Brand"].ToString(), fields["Model"].ToString(), fields["Description"].ToString(), Convert.ToDouble(fields["CurrentStockCount"]), Convert.ToDouble(fields["ReStockThreshold"]));
        }

        public IEnumerable<Equipment> GetAll()
        {
            connection();
            List<Equipment> EquipmentList = new List<Equipment>();


            SqlCommand cmd = new SqlCommand("GetAllEquipments", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                EquipmentList.Add(

                    new Equipment(Convert.ToInt32(dr["EquipId"]), dr["Brand"].ToString(), dr["Model"].ToString(), dr["Description"].ToString(), Convert.ToDouble(dr["CurrentStockCount"]), Convert.ToDouble(dr["ReStockThreshold"]))
                    );
            }

            return EquipmentList;
        }

        public bool Update(Equipment equipment)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateEquipment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipId", equipment.EquipId);
            cmd.Parameters.AddWithValue("@Brand", equipment.Brand);
            cmd.Parameters.AddWithValue("@Model", equipment.Model);
            cmd.Parameters.AddWithValue("@Description", equipment.Description);
            cmd.Parameters.AddWithValue("@CurrentStockCount", equipment.CurrentStockCount);
            cmd.Parameters.AddWithValue("@ReStockThreshold", equipment.ReStockThreshold);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public IEnumerable<Equipment> GetAllEquipmentsInStock()
        {
            List<Equipment> EquipmentsInStock = new List<Equipment>();

            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("SELECT * FROM[dbo].[Equipment] WHERE CurrentStockCount > 0", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var equipment = new Equipment();
                    equipment.EquipId = Convert.ToInt32(reader["EquipId"]);
                    equipment.Brand = reader["Brand"].ToString();
                    equipment.Model = reader["Model"].ToString();
                    equipment.Description = reader["Description"].ToString();
                    equipment.CurrentStockCount = Convert.ToInt32(reader["CurrentStockCount"]);
                    equipment.ReStockThreshold = Convert.ToInt32(reader["ReStockThreshold"]);
                    EquipmentsInStock.Add(equipment);
                }

                return EquipmentsInStock;

            }
            catch(Exception e)
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