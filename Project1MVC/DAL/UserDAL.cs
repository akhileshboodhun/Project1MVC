﻿using Project1MVC.Models;
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
    public class UserDAL : IManageDAL<User>
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ItStockDBConnection"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW SUPPLIER *********************
        public bool Add(User user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FName", user.FName);
            cmd.Parameters.AddWithValue("@LName", user.LName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);
            cmd.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }


        public User Get(int userId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("GetUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            var fields = dt.Rows[0];
            return new User(
                Convert.ToInt32(fields["UserId"]),
                fields["FName"].ToString(),
                fields["LName"].ToString(),
                fields["Email"].ToString(),
                fields["HashedPassword"].ToString(),
                Convert.ToInt32(fields["UserRoleId"])
                );
        }

        public IEnumerable<User> GetAll()
        {
            connection();
            List<User> UserList = new List<User>();


            SqlCommand cmd = new SqlCommand("GetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                UserList.Add(

                    new User(
                Convert.ToInt32(dr["UserId"]),
                dr["FName"].ToString(),
                dr["LName"].ToString(),
                dr["Email"].ToString(),
                dr["HashedPassword"].ToString(),
                Convert.ToInt32(dr["UserRoleId"])
                )
                    );
            }

            return UserList;
        }

        public bool Update(User user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@FName", user.FName);
            cmd.Parameters.AddWithValue("@LName", user.LName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);
            cmd.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }

        public bool Delete(User user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", user.UserId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return (i >= 1);
        }
    }
}