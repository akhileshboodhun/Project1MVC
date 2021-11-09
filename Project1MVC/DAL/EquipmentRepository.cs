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
        readonly IDBProvider dbProvider;
        string rep = "Repository";

        public EquipmentRepository(IDBProvider provider)
        {
            dbProvider = provider;
        }

        public bool Add(Equipment obj)
        {
            bool status = false;

            try
            {
                SqlCommand cmd = ServicesHelper.GenerateInsertSQLCommand<Equipment>(obj, dbProvider);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"FAILED: {MethodBase.GetCurrentMethod().Name} {this.GetType().Name.Replace(rep, "")}");
                Logger.Log($"{ex.ToString()}");
            }
            finally
            {
                dbProvider.Connection.Close();
            }

            return status;
        }

        public bool Delete(int id)
        {
            bool status = false;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
            //        string sql = "DELETE FROM Equipment WHERE EquipId = @Id;";

            //SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);
            //cmd.Parameters.AddWithValue("@Id", id);
                    
            //        try
            //        {
            //            if (cmd.ExecuteNonQuery() == 1)
            //            {
            //                status = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.Log($"FAILED: {opType} {modelName}");
            //            Logger.Log($"{ex.ToString()}");
            //        }
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
            return Get(id, ServicesHelper.GetColumns<Equipment>());
        }

        public Equipment Get(int id, IList<string> cols)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.Get;
            Equipment equipment = null;

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
            string sql = 
                "SELECT " +
                ServicesHelper.StringifyColumns<Equipment>(cols) + " " +
                "FROM Equipment WHERE EquipId = @Id;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipment = new Equipment();

                        foreach (string col in cols)
                        {
                            equipment[col] = reader[col];
                        }
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
            List<Equipment> list = new List<Equipment>();

            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
            //        string sql = "SELECT EquipId, Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold FROM Equipment;";

            //SqlCommand cmd = new SqlCommand(sql, dbProvider.GetConnection);

            //try
            //        {
            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    list.Add(new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.Log($"FAILED: {opType} {modelName}");
            //            Logger.Log($"{ex.ToString()}");
            //        }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            return list;
        }

        public IList<Equipment> GetPaginatedList(IList<string> cols, int? pageNumber, int? pageSize, string sortBy, string sortOrder)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("Repository", "");
            OperationType opType = OperationType.GetPaginated;
            List<Equipment> list = new List<Equipment>();
            
            //using (SqlConnection conn = dbProvider.GetConnection)
            //{
            //    if (conn != null)
            //    {
                    string sql =
                        "SELECT " +
                        ServicesHelper.StringifyColumns<Equipment>(cols) + " " +
                        "FROM Equipment " +
                        "ORDER BY [" + sortBy + "] " + sortOrder.ToUpper() + " " +
                        "OFFSET @Offset ROWS " +
                        "FETCH NEXT @PageSize ROWS ONLY;";

            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            cmd.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipment equipment = new Equipment();

                        foreach (string col in cols)
                        {
                            equipment[col] = reader[col];
                        }

                        list.Add(equipment);
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

            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);

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

            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
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