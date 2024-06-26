﻿using System;
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
    public sealed class EquipmentDAL : IModelDAL<Equipment>
    {
        private EquipmentDAL() { }

        public static EquipmentDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly EquipmentDAL instance = new EquipmentDAL();
        }

        public bool Add(Equipment obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "INSERT INTO Equipment (Type, Brand, Model, Description, CurrentStockCount, ReStockThreshold) " +
                        "VALUES (@Type, @Brand, @Model, @Description, @CurrentStockCount, @ReStockThreshold);";
           
                    SqlCommand cmd = new SqlCommand(sql, conn);
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
                    string sql = "DELETE FROM Equipment WHERE EquipId = @Id;";

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

        public Equipment Get(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            Equipment equipment = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT * FROM Equipment WHERE EquipId = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                equipment = new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt());
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

            return equipment;
        }

        public List<Equipment> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<Equipment> list = new List<Equipment>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT * FROM Equipment;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
        
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
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

        public bool Update(Equipment obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = 
                        "UPDATE Equipment " +
                        "SET Type=@Type, Brand=@Brand, Model=@Model, Description=@Description, CurrentStockCount=@CurrentStockCount, ReStockThreshold=@ReStockThreshold " + 
                        "WHERE EquipId = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
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

        public List<Equipment> GetAllEquipmentsInStock()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<Equipment> list = new List<Equipment>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = @"
                                  SELECT es.[SerialNo], eq.[EquipID], eq.[Type], eq.[Brand], eq.[Model], eq.[Description], eq.[ReStockThreshold]
                                  INTO #EquipmentInCurrentStock
                                FROM [EquipmentInStock] es JOIN [Equipment] eq ON es.[EquipID] = eq.[EquipID]
                                WHERE es.[SerialNo] NOT IN (
                                    SELECT [SerialNo]
                                    FROM [EquipmentEmployee]
                                    WHERE [DateReturned] IS NULL
                                )

                                SELECT DISTINCT [EquipID], [Type], [Brand], [Model], [Description], [ReStockThreshold], COUNT([SerialNo]) AS CurrentStockCount
                                FROM #EquipmentInCurrentStock
                                GROUP BY [EquipID], [Type], [Brand], [Model], [Description], [ReStockThreshold]
                               ";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new Equipment(reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
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

        public List<EquipmentInStock> GenerateEquipmentReport()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<EquipmentInStock> list = new List<EquipmentInStock>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = @"
                                  SELECT es.[SerialNo], eq.[EquipID], eq.[Type], eq.[Brand], eq.[Model], eq.[Description], eq.[ReStockThreshold]
                                  INTO #EquipmentInCurrentStock
                                FROM [EquipmentInStock] es JOIN [Equipment] eq ON es.[EquipID] = eq.[EquipID]
                                WHERE es.[SerialNo] NOT IN (
                                    SELECT [SerialNo]
                                    FROM [EquipmentEmployee]
                                    WHERE [DateReturned] IS NULL
                                )

                                SELECT [EquipId], COUNT([SerialNo]) AS NoAssigned
                                INTO #AssignedEquipments
                                FROM [EquipmentEmployee]
                                WHERE [DateReturned] IS NULL
                                GROUP BY [EquipId]

                                SELECT DISTINCT eics.[EquipID], eics.[Type], eics.[Brand], eics.[Model], eics.[Description], eics.[ReStockThreshold], COUNT(eics.[SerialNo]) AS CurrentStockCount, ae.NoAssigned
                                FROM #EquipmentInCurrentStock eics
                                LEFT JOIN #AssignedEquipments ae ON eics.EquipId = ae.EquipId
                                GROUP BY eics.[EquipID], eics.[Type], eics.[Brand], eics.[Model], eics.[Description], eics.[ReStockThreshold], ae.NoAssigned
                                ORDER BY eics.[Type], eics.[Brand], eics.[Model]
                               ";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                bool success = int.TryParse(reader["NoAssigned"].ToString(), out int noAssigned);
                                if (!success)
                                {
                                    noAssigned = 0;
                                }
                                list.Add(new EquipmentInStock(noAssigned, reader["EquipId"].ToInt(), reader["Type"].ToString(), reader["Brand"].ToString(), reader["Model"].ToString(), reader["Description"].ToString(), reader["CurrentStockCount"].ToInt(), reader["ReStockThreshold"].ToInt()));
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

    }
}