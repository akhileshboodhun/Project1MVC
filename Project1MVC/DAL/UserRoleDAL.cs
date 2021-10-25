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
    public class UserRoleDAL : IManageDAL<UserRole>
    {

      /*  private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        } */

        public bool Add(UserRole userRole)
        {
            var con = DBConnection.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("AddUserRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);


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

        public bool Delete(UserRole userRole)
        {
            var con = DBConnection.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);

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

        public UserRole Get(int userRoleId)
        {
            var con = DBConnection.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("GetRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserRoleId", userRoleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);

                var fields = dt.Rows[0];
                return new UserRole(Convert.ToInt32(fields["UserRoleId"]), fields["RoleName"].ToString());
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

        public IEnumerable<UserRole> GetAll()
        {
            var con = DBConnection.GetConnection();
            List<UserRole> UserRolesList = new List<UserRole>();

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllRoles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    UserRolesList.Add(

                        new UserRole(Convert.ToInt32(dr["UserRoleId"]), dr["RoleName"].ToString())
                        );
                }

                return UserRolesList;
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

        public bool Update(UserRole userRole)
        {
            var con = DBConnection.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateRole", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
                cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);

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