using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.DAL
{
    public sealed class UserRoleDAL : IModelDAL<UserRole>
    {
        private UserRoleDAL() { }

        public static UserRoleDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly UserRoleDAL instance = new UserRoleDAL();
        }

        public bool Add(UserRole obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "INSERT INTO [UserRole] ([RoleName]) " +
                        "VALUES (@RoleName);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@RoleName", obj.RoleName);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }

        public bool Delete(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Delete";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "DELETE FROM [UserRole] WHERE [UserRoleId] = @UserRoleId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserRoleId", id);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }

        public UserRole Get(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            UserRole UserRole = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT [UserRoleId], [RoleName] FROM [UserRole] WHERE [UserRoleId] = @UserRoleId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserRoleId", id);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                UserRole = new UserRole(reader["UserRoleId"].ToInt(), reader["RoleName"].ToString());
                            }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return UserRole;
        }

        public UserRole GetRoleByName(string roleName)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            UserRole UserRole = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT [UserRoleId], [RoleName] FROM [UserRole] WHERE [RoleName] = @RoleName;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@RoleName", roleName);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                UserRole = new UserRole(reader["UserRoleId"].ToInt(), reader["RoleName"].ToString());
                            }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return UserRole;
        }

        public List<UserRole> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<UserRole> list = new List<UserRole>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    
                    string sql = @" SELECT [UserRoleId], [RoleName]
                                    FROM [UserRole]
                                    ORDER BY [RoleName] ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new UserRole(reader["UserRoleId"].ToInt(), reader["RoleName"].ToString()));
                            }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return list;
        }

        public bool Update(UserRole obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if(conn.State == System.Data.ConnectionState.Closed) conn.Open();
                if (conn != null)
                {
                    string sql =
                        "UPDATE [UserRole] " +
                        "SET [RoleName]=@RoleName " +
                        "WHERE [UserRoleId] = @UserRoleId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserRoleId", obj.UserRoleId);
                    cmd.Parameters.AddWithValue("@RoleName", obj.RoleName);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }
    }

}