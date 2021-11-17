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

        public bool Delete(Equipment obj)
        {
            return false;
        }

        public Equipment Get(Equipment obj, IList<string> cols)
        {
            Equipment equipment = null;
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);

            if (_cols.Count == 0)
            {
                return null;
            }

            try
            {
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForGet<Equipment>(obj, _cols, dbProvider);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipment = new Equipment();

                        foreach (string col in _cols)
                        {
                            equipment[col] = reader[col];
                        }
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

            return equipment;
        }

        public IList<Equipment> GetAll(IList<string> cols)
        {
            List<Equipment> list = new List<Equipment>();
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);

            if (_cols.Count == 0)
            {
                return list;
            }

            try
            {
                SqlCommand cmd = ServicesHelper.GenerateSqlCommandForGetAll<Equipment>(dbProvider, _cols);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipment equipment = new Equipment();

                        foreach (string col in _cols)
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
            }

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

            bool primaryKeyInjected = false;
            string primaryColumn = ServicesHelper.GetDefaultColumn<Equipment>();
            
            if (!_cols.Contains(primaryColumn))
            {
                _cols.Add(primaryColumn);
                primaryKeyInjected = true;
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

                        foreach (string col in _cols)
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

                if (primaryKeyInjected)
                {
                    pgInfo.DisplayColumns.Remove(primaryColumn);
                }

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