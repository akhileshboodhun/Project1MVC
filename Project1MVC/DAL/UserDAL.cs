using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public sealed class UserDAL : IModelDAL<User>
    {
        private UserDAL() { }

        public static UserDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly UserDAL instance = new UserDAL();
        }

        public bool Add(User obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "INSERT INTO [User] ([FName], [LName], [Email], [Salt], [HashedPassword], [UserRoleId]) " +
                        "VALUES (@FName, @LName, @Email, @Salt, @HashedPassword, @UserRoleId);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@FName", obj.FName);
                    cmd.Parameters.AddWithValue("@LName", obj.LName);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Salt", obj.Salt);
                    cmd.Parameters.AddWithValue("@HashedPassword", obj.HashedPassword);
                    cmd.Parameters.AddWithValue("@UserRoleId", obj.UserRoleId);

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
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
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
                    string sql = "DELETE FROM [User] WHERE [UserId] = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

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

        public User Get(int id) => Get(id.ToString());
        public User Get(string query)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            User User = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    var byId = "[UserId] = @Id";
                    var byEmail = "[Email] = @Email";
                    int id;
                    var byCondition = (int.TryParse(query, out id)) ? byId : byEmail;
                    string sql = $"SELECT u.[UserId], u.[FName], u.[LName], u.[Email], u.[Salt], u.[HashedPassword], ur.[UserRoleId], ur.[RoleName] FROM [User] u JOIN [UserRole] ur ON  u.UserRoleId = ur.UserRoleId WHERE {byCondition};";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Email", query);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                User = new User(reader["UserId"].ToInt(),reader["FName"].ToString(), reader["LName"].ToString(), reader["Email"].ToString(), reader["Salt"].ToString(), reader["HashedPassword"].ToString(), reader["UserRoleId"].ToInt(), reader["RoleName"].ToString());
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

            return User;
        }

        public List<User> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<User> list = new List<User>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT u.[UserId], u.[FName], u.[LName], u.[Email], u.[Salt], u.[HashedPassword], ur.[UserRoleId], ur.[RoleName] FROM [User] u JOIN [UserRole] ur ON  u.UserRoleId = ur.UserRoleId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new User(reader["UserId"].ToInt(), reader["FName"].ToString(), reader["LName"].ToString(), reader["Email"].ToString(), reader["Salt"].ToString(), reader["HashedPassword"].ToString(), reader["UserRoleId"].ToInt(), reader["RoleName"].ToString())); }
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

        public bool Update(User obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "UPDATE [User] " +
                        @"SET [FName] = @FName,
                            [LName] = @LName,
                            [Email] = @Email,
                            [Salt] = @Salt,
                            [HashedPassword] = @HashedPassword,
                            [UserRoleId] = @UserRoleId" +
                        " WHERE [UserId] = @UserId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@FName", obj.FName);
                    cmd.Parameters.AddWithValue("@LName", obj.LName);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Salt", obj.Salt);
                    cmd.Parameters.AddWithValue("@HashedPassword", obj.HashedPassword);
                    cmd.Parameters.AddWithValue("@UserRoleId", obj.UserRoleId);

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