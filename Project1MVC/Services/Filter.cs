using System;
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
            // TODO: check if startswith and endswith { and } respectively
            Regex regCSV = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();

            foreach (Match match in regCSV.Matches(filterString.TrimStart('{').TrimEnd('}')))
            {
                string field = match.Value.TrimStart(',').TrimStart('"').TrimEnd('"');
                list.Add(field.Replace("\"", ""));
            }

            // TODO: check if list.count == 4
            // TODO: check if if startswith and endswith [ and ] respectively
            string col = list[0].Trim();
            string type = list[1].Trim();
            string s1 = list[2].Trim();
            string s2 = list[3].Trim();

            // TODO: check if enum is valid
            FilterType _filterType = (FilterType)Enum.Parse(typeof(FilterType), type);

            if (!(s1 == "" && s2 == ""))
            {
                return new Filter(col, s1, s2, _filterType);
            }
            else
            {
                return null;
            }
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