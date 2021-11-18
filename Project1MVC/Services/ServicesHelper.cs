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
        public static int PageIncrement = 3;

        public static int DefaultPageSize
        {
            get
            {
                return GetPageSizeList()[0];
            }
        }

        public static IList<int> GetPageSizeList()
        {
            List<int> list = new List<int>()
            {2, 4, 10, 25, 50, 100, 250, 500, 1000};

            list.Sort();
            return list;
        }

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

        public static IList<string> SanitizeColumns<T>(IList<string> cols, bool tolerateCalculatedColumns = true)
        {
            IList<string> colsParam = cols == null ? new List<string>() : cols;
            IList<string> _cols = new List<string>();
            IList<string> validCols = GetColumns<T>(tolerateCalculatedColumns);

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

        public static int SanitizePageNumber(string pageNumber)
        {
            int _pageNumber = (pageNumber != null && pageNumber.All(char.IsDigit)) ? pageNumber.ToInt() : 1;
            return (_pageNumber > 0) ? _pageNumber : 1;
        }

        public static int SanitizePageSize(string pageSize)
        {
            int _pageSize = (pageSize != null && pageSize.All(char.IsDigit)) ? pageSize.ToInt() : DefaultPageSize;
            return (_pageSize > 0) ? _pageSize : DefaultPageSize;
        }

        public static string StringifyColumns<T>(IList<string> cols, bool prefixWithTableName = false)
        {
            if (cols == null | cols.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            foreach (string col in cols)
            {
                string str = $"{col}, ";
                str = prefixWithTableName ? $"{typeof(T).Name}.{str}" : str;
                sb.Append(str);
            }

            return sb.ToString().Substring(0, sb.Length - 2);
        }

        public static IList<string> GetColumns<T>(bool includePrimaryKey = true, bool includeCalculatedColumns = true)
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

                // TODO: Check for ICalculatedAttribute instead
                if (!includeCalculatedColumns && Attribute.IsDefined(property, typeof(DirectCountAttribute)))
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

            foreach (PropertyInfo property in typeof(T).GetProperties())
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

        private static IDictionary<string, string> SeparateColumns<T>(IList<string> cols, out IList<string> cols_native, out IDictionary<string, string> cols_calc)
        {
            IList<string> _cols_native = new List<string>();
            IDictionary<string, string> _cols_calc = new Dictionary<string, string>();
            IDictionary<string, string> _cols_calc_unaliased = new Dictionary<string, string>();

            if (cols == null || cols.Count == 0)
            {
                cols_native = _cols_native;
                cols_calc = _cols_calc;
                return _cols_calc_unaliased;
            }

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name == "Item" || !cols.Contains(property.Name))
                {
                    continue;
                }

                // TODO: Handle other types of ICalculatedAttribute later
                if (Attribute.IsDefined(property, typeof(DirectCountAttribute)))
                {
                    object[] attributes = property.GetCustomAttributes(typeof(DirectCountAttribute), false);

                    if (attributes != null && attributes.Length > 0)
                    {
                        string c_table = typeof(T).Name;
                        string f_table = attributes.Cast<DirectCountAttribute>().Single().ForeignTable;
                        string f_col = attributes.Cast<DirectCountAttribute>().Single().ForeignColumn;
                        string f_key = attributes.Cast<DirectCountAttribute>().Single().ForeignKey;

                        string col_unaliased = $"COUNT({f_table}.{f_col})";
                        string col = $"{col_unaliased} AS [{property.Name}]";
                        string join = $"LEFT JOIN [{f_table}] ON [{c_table}].{f_key} = [{f_table}].{f_key}";

                        _cols_calc.Add(col, join);
                        _cols_calc_unaliased.Add(property.Name, col_unaliased);
                    }
                }
                else
                {
                    _cols_native.Add(property.Name);
                }
            }

            cols_native = _cols_native;
            cols_calc = _cols_calc;
            return _cols_calc_unaliased;
        }

        private static string GenerateConditionalClauseFromFiltersList(IList<Filter> filters, bool orFilters, bool forWhereClause = true, IDictionary<string, string> aliases = null)
        {
            string clause = "";
            string chain = orFilters ? " OR " : " AND ";
            string operand = forWhereClause ? "WHERE" : "HAVING";

            if (filters != null && filters.Count != 0)
            {
                StringBuilder sb_clause = new StringBuilder();
                sb_clause.Append($"{operand} (");

                for (int i = 0; i < filters.Count; i++)
                {
                    Filter filter = filters[i];
                    string filterCol = filter.ColumnName;
                    string s1 = filter.SearchValue1.Trim();
                    string s2 = filter.SearchValue2.Trim();
                    string op = "";

                    if (operand == "HAVING" && aliases != null)
                    {
                        filterCol = aliases[filter.ColumnName];
                    }

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

                    sb_clause.Append("(");
                    sb_clause.Append($"{filterCol} ");
                    sb_clause.Append(op);
                    sb_clause.Append(")");

                    // TODO: handle case for when previous filter is invalid => we're adding "chain" in 
                    // advance. => get all ops first in a list, then add chain.
                    if (i != filters.Count - 1)
                    {
                        sb_clause.Append(chain);
                    }
                }

                sb_clause.Append(")");
                clause = sb_clause.ToString();
            }

            return clause;
        }

        private static SqlCommand GenerateSqlCommand<T>(string sql, Model<T> obj, IDBProvider dbProvider)
        {
            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            IList<string> cols = GetColumns<T>(true, false);

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
            IList<string> colsParam = cols ?? GetColumns<T>(includePrimaryKey, false);
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

            colsParam = colsParam.Count != 0 ? colsParam : GetColumns<T>(includePrimaryKey, false);
            string _cols = StringifyColumns<T>(colsParam);
            string _colsParameterized = StringifyColumns<T>(FormatList(colsParam, "@"));

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"INSERT INTO [{typeof(T).Name}] ({_cols}) ");
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

        private static string GenerateSqlQueryForUpdate<T>(DBMS dbms, IList<string> cols = null)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> _cols = cols ?? GetColumns<T>(false, false);
            string primaryColumn = GetDefaultColumn<T>();

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"UPDATE [{typeof(T).Name}] ");
                    sb.Append($"SET ");
                    foreach (string col in _cols)
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

        public static SqlCommand GenerateSqlCommandForUpdate<T>(Model<T> obj, IDBProvider dbProvider, IList<string> cols = null)
        {
            string sql = GenerateSqlQueryForUpdate<T>(dbProvider.DBMS, cols);
            return GenerateSqlCommand(sql, obj, dbProvider);
        }

        private static string GenerateSqlQueryForGetPaginatedList<T>(DBMS dbms, IList<string> cols, string sortBy = "", string sortOrder = "", IList<Filter> filters = null, bool orFilters = true)
        {
            StringBuilder sb = new StringBuilder();
            string _table = typeof(T).Name;
            string _sortBy = SanitizeSortBy<T>(sortBy);
            string _sortOrder = SanitizeSortOrder(sortOrder).ToUpper();
            string _whereClause = GenerateConditionalClauseFromFiltersList(filters, orFilters);

            IList<string> cols_native;
            IDictionary<string, string> cols_calc;
            IDictionary<string, string> cols_calc_unaliased = SeparateColumns<T>(cols, out cols_native, out cols_calc);

            string _havingClause = GenerateConditionalClauseFromFiltersList(filters, orFilters, false, cols_calc_unaliased);

            string _cols = StringifyColumns<T>(cols_native, cols_calc.Count != 0);
            _cols = (cols_calc.Count == 0) ? _cols : _cols + ", " + StringifyColumns<T>(cols_calc.Keys.ToList(), false);

            string _groupBy = StringifyColumns<T>(cols_native, true);

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT {_cols} ");
                    sb.Append($"FROM [{_table}] ");

                    if (cols_calc.Count == 0)
                    {
                        sb.Append(_whereClause == "" ? "" : $"{_whereClause} ");
                    }
                    else
                    {
                        foreach (string join in cols_calc.Values.Distinct().ToList())
                        {
                            sb.Append($"{join} ");
                        }

                        sb.Append($"GROUP BY {_groupBy} ");
                        sb.Append(_havingClause == "" ? "" : $"{_havingClause} ");
                    }

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
            string _whereClause = GenerateConditionalClauseFromFiltersList(filters, orFilters);
            _whereClause = _whereClause != "" ? $" {_whereClause}" : "";

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT COUNT({_primaryColumn}) AS [Total] ");
                    sb.Append($"FROM [{_table}] ");
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

        private static string GenerateSqlQueryForGet<T>(DBMS dbms, IList<string> cols)
        {
            StringBuilder sb = new StringBuilder();
            string _table = typeof(T).Name;
            string _cols = StringifyColumns<T>(SanitizeColumns<T>(cols));
            string primaryColumn = GetDefaultColumn<T>();

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT ");
                    sb.Append($"{_cols} ");
                    sb.Append($"FROM [{_table}] ");
                    sb.Append($" WHERE ({primaryColumn} = @{primaryColumn});");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForGet<T>(Model<T> obj, IList<string> cols, IDBProvider dbProvider)
        {
            string sql = GenerateSqlQueryForGet<T>(dbProvider.DBMS, cols);
            return GenerateSqlCommand(sql, obj, dbProvider);
        }

        private static string GenerateSqlQueryForGetAll<T>(DBMS dbms, IList<string> cols)
        {
            StringBuilder sb = new StringBuilder();
            string _cols = StringifyColumns<T>(SanitizeColumns<T>(cols));
            string _table = typeof(T).Name;

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT {_cols} ");
                    sb.Append($"FROM [{_table}];");
                    break;

                default:
                    throw new InvalidOperationException("Invalid value for parameter 'dbms'");
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForGetAll<T>(IDBProvider dbProvider, IList<string> cols)
        {
            string sql = GenerateSqlQueryForGetAll<T>(dbProvider.DBMS, cols);
            return new SqlCommand(sql, dbProvider.Connection);
        }
    }
}