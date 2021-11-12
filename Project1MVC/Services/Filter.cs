using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class Filter
    {
        public Filter(string columnName, string searchValue1, string searchValue2, FilterType type)
        {
            ColumnName = columnName;
            FilterType = type;
            SearchValue1 = searchValue1;
            SearchValue2 = searchValue2;
        }

        public string ColumnName
        {
            get; private set;
        }

        public string SearchValue1
        {
            get; private set;
        }

        public string SearchValue2
        {
            get; private set;
        }

        public FilterType FilterType
        {
            get; private set;
        }

        public override string ToString()
        {
            return $"{{[{ColumnName}],[{SearchValue1}],[{SearchValue2}],[{FilterType}]}}";
        }

        public static Filter FromString(string filterString)
        {
            // TODO: check if startswith and endswith { and } respectively
            List<string> list = filterString.TrimStart('{').TrimEnd('}').Split(',').ToList();

            // TODO: check if list.count == 4
            // TODO: check if if startswith and endswith [ and ] respectively
            string col = list[0].TrimStart('[').TrimEnd(']');
            string type = list[1].TrimStart('[').TrimEnd(']');
            string s1 = list[2].TrimStart('[').TrimEnd(']');
            string s2 = list[3].TrimStart('[').TrimEnd(']');
            
            // TODO: check if enum is valid
            return new Filter(col, s1, s2, (FilterType)Enum.Parse(typeof(FilterType), type));
        }

        public static IList<Filter> FromComplexString(string complexString)
        {
            IList<Filter> list = new List<Filter>();
                
            foreach(string filter in complexString.Split('|'))
            {
                list.Add(Filter.FromString(filter));
            }

            return list;
        }
    }
}