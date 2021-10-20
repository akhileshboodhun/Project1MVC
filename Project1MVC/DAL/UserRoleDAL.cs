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
    public class UserRoleDAL : IManageDAL<UserRole>
    {

        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        public bool Add(UserRole userRole)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddUserRole", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public bool Delete(UserRole userRole)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteRole", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public UserRole Get(int userRoleId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("GetRole", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserRoleId", userRoleId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            var fields = dt.Rows[0];
            return new UserRole(Convert.ToInt32(fields["UserRoleId"]), fields["RoleName"].ToString());
        }

        public IEnumerable<UserRole> GetAll()
        {
            connection();
            List<UserRole> UserRolesList = new List<UserRole>();


            SqlCommand cmd = new SqlCommand("GetAllRoles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                UserRolesList.Add(

                    new UserRole(Convert.ToInt32(dr["UserRoleId"]), dr["RoleName"].ToString())
                    );
            }

            return UserRolesList;
        }

        public bool Update(UserRole userRole)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateRole", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
            cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }
    }
}