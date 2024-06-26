﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            return $"{{\"{ColumnName}\", \"{FilterType}\", \"{SearchValue1}\", \"{SearchValue2}\"}}";
        }

        public static Filter FromString(string filterString)
        {
            if (!(filterString.StartsWith("{") && filterString.EndsWith("}")))
            {
                return null;
            }

            Regex regCSV = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();

            foreach (Match match in regCSV.Matches(filterString.TrimStart('{').TrimEnd('}')))
            {
                string field = match.Value.TrimStart(',').TrimStart('"').TrimEnd('"');
                list.Add(field.Replace("\"", ""));
            }

            if (list.Count != 4)
            {
                return null;
            }

            string col = list[0].Trim();
            string type = list[1].Trim();
            string s1 = list[2].Trim();
            string s2 = list[3].Trim();

            if (!Enum.IsDefined(typeof(FilterType), type))
            {
                return null;
            }

            FilterType _filterType = (FilterType)Enum.Parse(typeof(FilterType), type);

            if (_filterType == FilterType.Range)
            {
                if (s1 != "" && !s1.All(char.IsDigit))
                {
                    return null;
                }

                if (s2 != "" && !s2.All(char.IsDigit))
                {
                    return null;
                }
            }            

            if (!(s1 == "" && s2 == ""))
            {
                return new Filter(col, s1, s2, _filterType);
            }
            else
            {
                return null;
            }
        }

        public static IDictionary<string, string> GetFieldsDictionaryFromFiltersList<T>(IList<Filter> filters)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string col in ServicesHelper.GetColumns<T>())
            {
                dict.Add($"{col}S1", "");
                dict.Add($"{col}S2", "");
            }

            if (filters != null && filters.Count != 0)
            {
                foreach (Filter filter in filters)
                {
                    dict[$"{filter.ColumnName}S1"] = $"{filter.SearchValue1}";
                    dict[$"{filter.ColumnName}S2"] = $"{filter.SearchValue2}";
                }
            }

            return dict;
        }

        public static IList<Filter> FromComplexString(string complexString)
        {
            if (string.IsNullOrEmpty(complexString))
            {
                return null;
            }

            IList<Filter> list = new List<Filter>();

            foreach (string filterString in complexString.Split('|'))
            {
                if (!String.IsNullOrEmpty(filterString))
                {
                    Filter filter = Filter.FromString(filterString);
                    
                    if (filter != null)
                    {
                        list.Add(filter);
                    }
                }
            }

            return list;
        }
    }
}