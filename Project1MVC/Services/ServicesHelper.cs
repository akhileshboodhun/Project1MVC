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
        public static readonly int DefaultPageSize = 2; // TODO: set this to a higher value after testing

        public static string SanitizeSortOrder(string sortOrder)
        {
            return sortOrder.ToLower().StartsWith("de") ? "desc" : "asc";
        }

        public static string SanitizeSortBy<T>(string sortBy)
        {
            IList<string> validCols = GetColumns<T>();
            return validCols.Contains(sortBy) ? sortBy : GetDefaultColumn<T>();
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
            IList<string> _cols = new List<string>();
            IList<string> validCols = GetColumns<T>();

            foreach (string entry in cols)
            {
                if (validCols.Contains(entry))
                {
                    _cols.Add(entry);
                }
            }

            _cols = _cols.Count != 0 ? _cols : validCols;
            return _cols;
        }

        public static string StringifyColumns<T>(IList<string> cols, bool sanitize = true)
        {
            IList<string> _cols;
            IList<string> colsParam = cols ?? GetColumns<T>();
            StringBuilder sb = new StringBuilder();

            if (sanitize)
            {
                _cols = SanitizeColumns<T>(colsParam);
            }
            else
            {
                _cols = colsParam.Count != 0 ? colsParam : GetColumns<T>();
            }

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

        private static IList<string> FormatList(IList<string> cols, string prefix = "", string suffix = "")
        {
            IList<string> _cols = new List<string>();

            foreach (string col in cols)
            {
                _cols.Add($"{prefix}{col}{suffix}");
            }

            return _cols;
        }

        private static SqlCommand GenerateSqlCommand<T>(string sql, Model<T> obj, IDBProvider dbProvider, bool includePrimaryKey)
        {
            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            IList<string> cols = GetColumns<T>(includePrimaryKey);

            foreach (string col in cols)
            {
                cmd.Parameters.AddWithValue($"@{col}", obj[col]);
            }

            return cmd;
        }

        private static string GenerateSqlQueryForInsert<T>(DBMS dbms, bool includePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();
            IList<string> cols = GetColumns<T>(includePrimaryKey);

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"INSERT INTO {typeof(T).Name} ({StringifyColumns<T>(cols)}) ");
                    sb.Append($"VALUES ({StringifyColumns<T>(FormatList(cols, "@"), false)});");
                    break;
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForInsert<T>(Model<T> obj, IDBProvider dbProvider, bool includePrimaryKey = false)
        {
            string sql = GenerateSqlQueryForInsert<T>(dbProvider.DBMS, includePrimaryKey);
            return GenerateSqlCommand(sql, obj, dbProvider, includePrimaryKey);
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
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForUpdate<T>(Model<T> obj, IDBProvider dbProvider)
        {
            string sql = GenerateSqlQueryForUpdate<T>(dbProvider.DBMS);
            return GenerateSqlCommand(sql, obj, dbProvider, true);
        }

        private static string GenerateSqlQueryForGetPaginatedList<T>(IList<string> cols, string sortBy, string sortOrder, DBMS dbms)
        {
            StringBuilder sb = new StringBuilder();
            string _table = typeof(T).Name;
            string _cols = StringifyColumns<T>(cols);
            string _sortBy = SanitizeSortBy<T>(sortBy);
            string _sortOrder = SanitizeSortOrder(sortOrder).ToUpper();

            switch (dbms)
            {
                case DBMS.SQLServer:
                    sb.Append($"SELECT ");
                    sb.Append($"{_cols} ");
                    sb.Append($"FROM {_table} ");
                    sb.Append($"ORDER BY [{_sortBy}] {_sortOrder} ");
                    sb.Append($"OFFSET @Offset ROWS ");
                    sb.Append($"FETCH NEXT @PageSize ROWS ONLY;");
                    break;
            }

            return sb.ToString();
        }

        public static SqlCommand GenerateSqlCommandForGetPaginatedList<T>(IList<string> cols, int? pageNumber, int? pageSize, string sortBy, string sortOrder, IDBProvider dbProvider)
        {
            string sql = GenerateSqlQueryForGetPaginatedList<T>(cols, sortBy, sortOrder, dbProvider.DBMS);
            int _pageNumber = pageNumber ?? 1;
            int _pageSize = pageSize ?? DefaultPageSize;

            SqlCommand cmd = new SqlCommand(sql, dbProvider.Connection);
            cmd.Parameters.AddWithValue("@Offset", (_pageNumber - 1) * _pageSize);
            cmd.Parameters.AddWithValue("@PageSize", _pageSize);

            return cmd;
        }

        private static string GenerateSqlQueryForGet<T>(IList<string> cols, DBMS dbms)
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
            }

            string test = sb.ToString();
            return sb.ToString();
        }
    }
}