using Project1MVC.DAL;
using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Services
{
    public static class ServicesHelper
    {
        public static string SanitizeSortOrder(string sortOrder)
        {
            string defaultOrder = "asc";

            if (sortOrder != null)
            {
                return sortOrder.ToLower().StartsWith("de") ? "desc" : defaultOrder;

            }
            else
            {
                return defaultOrder;
            }
        }

        public static string SanitizeSortBy<T>(string sortBy)
        {
            IList<string> validCols = GetColumns<T>();
            string primaryColumn = GetDefaultColumn<T>();

            if (sortBy != null)
            {
                return validCols.Contains(sortBy) ? sortBy : primaryColumn;
            }
            else
            {
                return primaryColumn;
            }
            
        }

        public static IDictionary<string, string> GetNextSortParams<T>(string sortBy, string sortOrder)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                dict.Add(property.Name, "asc");
            }

            if (sortOrder.ToLower().StartsWith("de"))
            {
                dict[sortBy] = "asc";
            }
            else
            {
                dict[sortBy] = "desc";
            }

            return dict;
        }

        public static IList<string> SanitizeColumns<T>(IList<string> cols)
        {
            IList<string> colsParam = cols == null ? new List<string>() : cols;
            IList<string> _cols = new List<string>();
            IList<string> validCols = GetColumns<T>();

            foreach (string entry in colsParam)
            {
                if (validCols.Contains(entry))
                {
                    _cols.Add(entry);
                }
            }

            return _cols;
        }

        public static string SanitizeString(string str)
        {
            return str != null ? str : "";
        }

        public static bool SanitizeBoolean(string str)
        {
            return (str != null && str.ToString().Trim().ToLower() == "false") ? false : true;
        }

        public static string StringifyColumns<T>(IList<string> cols = null, bool sanitize = true)
        {
            IList<string> _cols = cols ?? GetColumns<T>();
            StringBuilder sb = new StringBuilder();

            if (sanitize)
            {
                _cols = SanitizeColumns<T>(_cols);
            }
               
            _cols = _cols.Count != 0 ? _cols : GetColumns<T>();
            
            foreach (string col in _cols)
            {
                sb.Append(col + ", ");
            }

            return sb.ToString().Substring(0, sb.Length - 2);
        }

        public static IList<string> GetColumns<T>(bool includePrimaryKey = true)
        {
            IList<string> list = new List<string>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name == "Item")
                {
                    continue;
                }

                if (!includePrimaryKey && Attribute.IsDefined(property, typeof(KeyAttribute)))
                {
                    continue;
                }

                list.Add(property.Name);
            }

            return list;
        }

        public static string GetDefaultColumn<T>()
        {
            string col = "";

            foreach(PropertyInfo property in typeof(T).GetProperties())
            {
                if (Attribute.IsDefined(property, typeof(KeyAttribute)))
                {
                    col = property.Name;
                    break;
                }
            }

            return col;
        }

        public static string GetDisplayName<T>(string propertyName)
        {
            string name = "null";

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.Name == propertyName)
                {
                    object[] attributes = info.GetCustomAttributes(typeof(DisplayAttribute), false);

                    if (attributes != null && attributes.Length > 0)
                    {
                        name = attributes.Cast<DisplayAttribute>().Single().Name;
                        break;
                    }
                }
            }

            return name;
        }

        public static bool IsColumnOfTypeString<T>(string columnName)
        {
            bool result = false;

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.Name == columnName)
                {
                    result = info.PropertyType == typeof(string);
                    break;
                }
            }

            return result;
        }

        private static IList<string> FormatList(IList<string> cols, string prefix = "", string suffix = "")
        {
            IList<string> _cols = new List<string>();

            foreach (string col in cols)
            {
                _cols.Add($"{prefix}{col}{suffix}");
            }

            return _cols;
        }
                
        private static string GenerateWhereClauseFromFiltersList(IList<Filter> filters, bool orFilters)
        {
            string whereClause = "";
            string chain = orFilters ? " OR " : " AND ";

            if (filters != null && filters.Count != 0)
            {
                StringBuilder sb_where = new StringBuilder();
                sb_where.Append("WHERE (");

                for (int i = 0; i < filters.Count; i++)
                {
                    Filter filter = filters[i];
                    string filterCol = filter.ColumnName;
                    string s1 = filter.SearchValue1.Trim();
                    string s2 = filter.SearchValue2.Trim();
                    string op = "";

                    if (filter.FilterType == FilterType.Contains)
                    {
                        op = $"LIKE '%{s1}%'";
                    }
                    else if (filter.FilterType == FilterType.Range)
                    {
                        if (s1 != "" && s2 != "" && s1.All(char.IsDigit) && s2.All(char.IsDigit))
                        {
                            op = $"BETWEEN {s1} AND {s2}";
                        }
                        else if (s1 != "" && s2 == "" && s1.All(char.IsDigit))
                        {
                            op = $">= {s1}";
                        }
                        else if (s1 == "" && s2 != "" && s2.All(char.IsDigit))
                        {
                            op = $"<= {s2}";
                        }
                    }

                    if (op == "")
                    {
                        continue;
                    }

                    sb_where.Append("(");
                    sb_where.Append($"{filterCol} ");
                    sb_where.Append(op);
                    sb_where.Append(")");

                    // TODO: handle case for when previous filter is invalid => we're adding "chain" in 
                    // advance. => get all ops first in a list, then add chain.
                    if (i != filters.Count - 1)
                    {
                        sb_where.Append(chain);
                    }
                }

                sb_where.Append(")");
                whereClause = sb_where.ToString();
            }

            return whereClause;
        }

        private static SqlCommand GenerateSqlCommand<T>(string sql, Model<T> obj, IDBProvider dbProvider)
        {
            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            IList<string> cols = GetColumns<T>();

            foreach (string col in cols)
            {
                string param = $"@{col}";

                if (cmd.CommandText.Contains(param))
                {
                    cmd.Parameters.AddWithValue(param, obj[col]);
                }
            }

            return cmd;
        }

        private static string GenerateSqlQueryForInsert<T>(DBMS dbms, IList<string> cols = null, bool includePrimaryKey = false)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> colsParam = cols ?? GetColumns<T>(includePrimaryKey);
            string primaryColumn = GetDefaultColumn<T>();

            if (!includePrimaryKey)
            {
                colsParam.Remove(primaryColumn);
            }
            else
            {
                if (!colsParam.Contains(primaryColumn))
                {
                    colsParam.Add(primaryColumn);
                }
            }

            colsParam = colsParam.Count != 0 ? colsParam : GetColumns<T>(includePrimaryKey);
            string _cols = StringifyColumns<T>(colsParam, false);
            string _colsParameterized = StringifyColumns<T>(FormatList(colsParam, "@"), false);

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"INSERT INTO {typeof(T).Name} ({_cols}) ");
                    sb.Append($"VALUES ({_colsParameterized});");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForInsert<T>(Model<T> obj, IDBProvider dbProvider, IList<string> cols = null, bool includePrimaryKey = false)
        {
            string sql = GenerateSqlQueryForInsert<T>(dbProvider.DBMS, cols, includePrimaryKey);
            return GenerateSqlCommand(sql, obj, dbProvider);
        }

        private static string GenerateSqlQueryForUpdate<T>(DBMS dbms)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> cols = GetColumns<T>(false);
            string primaryColumn = GetDefaultColumn<T>();

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"UPDATE {typeof(T).Name} ");
                    sb.Append($"SET ");
                    foreach (string col in cols)
                    {
                        sb.Append($"[{col}] = @{col}, ");
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append($" WHERE ({primaryColumn} = @{primaryColumn});");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForUpdate<T>(Model<T> obj, IDBProvider dbProvider)
        {
            string sql = GenerateSqlQueryForUpdate<T>(dbProvider.DBMS);
            return GenerateSqlCommand(sql, obj, dbProvider);
        }

        private static string GenerateSqlQueryForGetPaginatedList<T>(DBMS dbms, IList<string> cols, string sortBy = "", string sortOrder = "", IList<Filter> filters = null, bool orFilters = true)
        {
            StringBuilder sb = new StringBuilder();
            string _table = typeof(T).Name;
            string _cols = StringifyColumns<T>(cols);
            string _sortBy = SanitizeSortBy<T>(sortBy);
            string _sortOrder = SanitizeSortOrder(sortOrder).ToUpper();
            string _whereClause = GenerateWhereClauseFromFiltersList(filters, orFilters);

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT ");
                    sb.Append($"{_cols} ");
                    sb.Append($"FROM {_table} ");
                    sb.Append(_whereClause == "" ? "" : $"{_whereClause} ");
                    sb.Append($"ORDER BY [{_sortBy}] {_sortOrder} ");
                    sb.Append($"OFFSET @Offset ROWS ");
                    sb.Append($"FETCH NEXT @PageSize ROWS ONLY;");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForGetPaginatedList<T>(IDBProvider dbProvider, IList<string> cols, int pageNumber, int pageSize, string sortBy = "", string sortOrder = "", IList<Filter> filters = null, bool orFilters = true)
        {
            string sql = GenerateSqlQueryForGetPaginatedList<T>(dbProvider.DBMS, cols, sortBy, sortOrder, filters, orFilters);
                    
            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            cmd.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            return cmd;
        }

        private static string GenerateSqlQueryForGetCount<T>(DBMS dbms, IList<Filter> filters = null, bool orFilters = true)
        {
            StringBuilder sb = new StringBuilder();
            string _primaryColumn = GetDefaultColumn<T>();
            string _table = typeof(T).Name;
            string _whereClause = GenerateWhereClauseFromFiltersList(filters, orFilters);
            _whereClause = _whereClause != "" ? $" {_whereClause}" : "";

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT COUNT({_primaryColumn}) AS [Total] ");
                    sb.Append($"FROM {_table}");
                    sb.Append(_whereClause);
                    sb.Append(";");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForGetCount<T>(IDBProvider dbProvider, IList<Filter> filters = null, bool orFilters = true)
        {
            string sql = GenerateSqlQueryForGetCount<T>(dbProvider.DBMS, filters, orFilters);
            return new SqlCommand(sql, dbProvider.Connection);
        }

        private static string GenerateSqlQueryForGet<T>(DBMS dbms, IList<string> cols = null)
        {
            StringBuilder sb = new StringBuilder();
            string _table = typeof(T).Name;
            string _cols = StringifyColumns<T>(cols);
            string primaryColumn = GetDefaultColumn<T>();

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT ");
                    sb.Append($"{_cols} ");
                    sb.Append($"FROM {_table} ");
                    sb.Append($" WHERE ({primaryColumn} = @{primaryColumn});");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }
    }
}