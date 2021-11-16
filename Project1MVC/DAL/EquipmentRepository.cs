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
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForInsert<Equipment>(obj, dbProvider);

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

        public IList<Equipment> GetPaginatedList(out PaginatedListInfo<Equipment> paginatedListInfo, IList<string> cols, string pageNumber, string pageSize, string sortBy, string sortOrder, IList<Filter> filters = null, bool orFilters = true)
        {
            List<Equipment> list = new List<Equipment>();
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);

            if (_cols.Count == 0)
            {
                paginatedListInfo = new PaginatedListInfo<Equipment>(_cols, pageNumber, pageSize, 0, sortBy, sortOrder);
                return list;
            }

            int recordsCount = GetCount(filters, orFilters);
            PaginatedListInfo<Equipment> pgInfo = new PaginatedListInfo<Equipment>(_cols, pageNumber, pageSize, recordsCount, sortBy, sortOrder);
            
            try
            {
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForGetPaginatedList<Equipment>(dbProvider, pgInfo.DisplayColumns, pgInfo.PageNumber, pgInfo.PageSize, pgInfo.SortBy, pgInfo.SortOrder, filters, orFilters);

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
                Logger.Log($"FAILED: {MethodBase.GetCurrentMethod().Name} {this.GetType().Name.Replace(rep, "")}");
                Logger.Log($"{ex.ToString()}");
            }
            finally
            {
                dbProvider.Connection.Close();
                paginatedListInfo = pgInfo;
            }
            
            return list;
        }

        public int GetCount(IList<Filter> filters = null, bool orFilters = true)
        {
            int count = 0;

            try
            {
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForGetCount<Equipment>(dbProvider, filters, orFilters);

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
                Logger.Log($"FAILED: {MethodBase.GetCurrentMethod().Name} {this.GetType().Name.Replace(rep, "")}");
                Logger.Log($"{ex.ToString()}");
            }
            finally
            {
                dbProvider.Connection.Close();
            }

            return count;
        }

        public bool Update(Equipment obj)
        {
            bool status = false;

            try
            {
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForUpdate<Equipment>(obj, dbProvider);

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
    }
}