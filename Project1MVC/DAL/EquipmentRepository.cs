using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public sealed class EquipmentRepository : IRepository<Equipment>
    {
        //private EquipmentRepository() { }

        //public static EquipmentRepository Instance { get { return Nested.instance; } }

        //private class Nested
        //{
        //    // Explicit static constructor to tell C# compiler not to lazily instantiate us
        //    static Nested() { }

        //    internal static readonly EquipmentRepository instance = new EquipmentRepository();
        //}

        private readonly IDBProvider dbProvider;

        public EquipmentRepository(IDBProvider provider)
        {
            dbProvider = provider;
        }

        public bool Add(Equipment obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.Add;
            bool status = false;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql =
                        "INSERT INTO Equipment (Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold) " +
                        "VALUES (@Type, @Brand, @Model, @Description, @CurrentStockCount, @ReStockThreshold);";
 
                    SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
                    cmd.Parameters.AddWithValue("@Type", obj.Type);
                    cmd.Parameters.AddWithValue("@Brand", obj.Brand);
                    cmd.Parameters.AddWithValue("@Model", obj.Model);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@CurrentStockCount", obj.CurrentStockCount);
                    cmd.Parameters.AddWithValue("@ReStockThreshold", obj.ReStockThreshold);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    //finally
                    //{
                    //    transaction.Connection.Close();
                    //}
            //    }
            //}         

            return status;
        }

        public bool Delete(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.Delete;
            bool status = false;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql = "DELETE FROM Equipment WHERE EquipId = @Id;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
            cmd.Parameters.AddWithValue("@Id", id);
                    
                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    //finally
                    //{
                    //    conn.Close();
                    //}
            //    }
            //}

            return status;
        }

        public Equipment Get(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.Get;
            Equipment equipment = null;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql = "SELECT EquipId, Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold FROM Equipment WHERE EquipId = @Id;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
            cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipment = new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return equipment;
        }

        public IList<Equipment> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.GetAll;
            List<Equipment> list = new List<Equipment>();

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql = "SELECT EquipId, Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold FROM Equipment;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);

            try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return list;
        }

        public IList<Equipment> GetPaginatedList(int? pageNumber, int? pageSize, string sortBy, string sortOrder)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.GetPaginated;
            List<Equipment> list = new List<Equipment>();
            
            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql =
                        "SELECT EquipId, Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold " +
                        "FROM Equipment " +
                        "ORDER BY [" + sortBy + "] " + sortOrder.ToUpper() + " " +
                        "OFFSET @Offset ROWS " +
                        "FETCH NEXT @PageSize ROWS ONLY;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
            cmd.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return list;
        }

        public int GetCount()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.GetCount;
            int count = -1;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql = "SELECT COUNT(EquipId) AS [Total] FROM Equipment;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);

            try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader["Total"].ToInt();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return count;
        }

        public bool Update(Equipment obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.Update;
            bool status = false;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql = 
                        "UPDATE Equipment " +
                        "SET Type=@Type, Brand=@Brand, Model=@Model, Description=@Description, CurrentStockCount=@CurrentStockCount, ReStockThreshold=@ReStockThreshold " + 
                        "WHERE EquipId = @Id;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
            cmd.Parameters.AddWithValue("@Id", obj.EquipId);
                    cmd.Parameters.AddWithValue("@Type", obj.Type);
                    cmd.Parameters.AddWithValue("@Brand", obj.Brand);
                    cmd.Parameters.AddWithValue("@Model", obj.Model);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@CurrentStockCount", obj.CurrentStockCount);
                    cmd.Parameters.AddWithValue("@ReStockTreshold", obj.ReStockThreshold);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return status;
        }
    }
}