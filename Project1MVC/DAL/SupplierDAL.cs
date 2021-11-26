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
    public sealed class SupplierDAL : IModelDAL<Supplier>
    {
        private SupplierDAL() { }

        public static SupplierDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly SupplierDAL instance = new SupplierDAL();
        }

        public bool Add(Supplier obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "INSERT INTO [Supplier] ([Name], [PhoneNo], [Address]) " +
                        "VALUES (@Name, @PhoneNo, @Address);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);

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
                    string sql = "DELETE FROM [Supplier] WHERE [SupplierId] = @Id;";

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

        public Supplier Get(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            Supplier Supplier = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT * FROM [Supplier] WHERE [SupplierId] = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                Supplier = new Supplier(reader["SupplierId"].ToInt(), reader["Name"].ToString(), reader["PhoneNo"].ToString(), reader["Address"].ToString());
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

            return Supplier;
        }

        public List<Supplier> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<Supplier> list = new List<Supplier>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT [SupplierId], [Name], [PhoneNo], [Address] FROM [Supplier] ORDER BY [Name];";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new Supplier(reader["SupplierId"].ToInt(), reader["Name"].ToString(), reader["PhoneNo"].ToString(), reader["Address"].ToString()));
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

        public bool Update(Supplier obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "UPDATE [Supplier] " +
                        "SET [Name]=@Name, [PhoneNo]=@PhoneNo, [Address]=@Address " +
                        "WHERE [SupplierId] = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", obj.SupplierId);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);

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