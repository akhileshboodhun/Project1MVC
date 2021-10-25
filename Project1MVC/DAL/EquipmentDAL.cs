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
                        "VALUES (@Type, @Brand, @Model, @Description, @CurrentStockCount, @ReStockThreshold)";
           
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
            throw new NotImplementedException();
        }

        public Equipment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipment> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Equipment obj)
        {
            throw new NotImplementedException();
        }
    }
}