using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

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
            Dictionary<string, string> dict = new Dictionary<string, string>();

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

        public static string StringifyColumns<T>(IList<string> cols)
        {
            if (cols.Count == 0)
            {
                return "* ";
            }

            StringBuilder sb = new StringBuilder();
            
            foreach (string col in cols)
            {
                sb.Append(col + ", ");
            }

            return sb.ToString().Remove(sb.Length - 2, 1);
        }

        public static IList<string> GetColumns<T>()
        {
            List<string> list = new List<string>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                list.Add(property.Name);
            }

            return list;
        }

        private static string GetDefaultColumn<T>()
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
    }
}