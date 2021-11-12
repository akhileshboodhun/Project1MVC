﻿using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.DAL
{
    public sealed class FutureStockDAL
    {
        private FutureStockDAL() { }

        public static FutureStockDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly FutureStockDAL instance = new FutureStockDAL();
        }

        public List<FutureStock> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<FutureStock> list = new List<FutureStock>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT [Id], [Brand], [Model], [OrderDate], [UnitPrice], [Qty], [NetPrice], [SupplierName] FROM [FutureStockView];";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new FutureStock(reader["Id"].ToInt(), reader["Brand"].ToString(), reader["Model"].ToString(), Convert.ToDateTime(reader["OrderDate"].ToString()), Convert.ToDouble(reader["UnitPrice"]), Convert.ToInt32(reader["Qty"]), Convert.ToDouble(reader["NetPrice"]), reader["SupplierName"].ToString()));
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