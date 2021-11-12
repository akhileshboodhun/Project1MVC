using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class Filter
    {
        public Filter(string columnName, object searchValue1, object searchValue2, FilterType type)
        {
            ColumnName = columnName;
            SearchValue1 = searchValue1;
            SearchValue2 = searchValue2;
            FilterType = type;

            if (type == FilterType.Range)
            {
                if (searchValue2 == null && searchValue1 != null)
                {
                    SearchValue1 = searchValue1;
                    SearchValue2 = int.MaxValue;
                }
                else if (searchValue1 == null && searchValue2 != null)
                {
                    SearchValue1 = int.MinValue;
                    SearchValue2 = searchValue2;
                }
                else if (searchValue1 == null && searchValue2 == null)
                {
                    throw new InvalidOperationException("Both searchValue1 and searchValue2 cannot be null when FilterType=Range is used.");
                }
            }

            
        }

        public string ColumnName
        {
            get; private set;
        }

        public object SearchValue1
        {
            get; private set;
        }

        public object SearchValue2
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

        public Filter FromString(string filterString)
        {
            // TODO: check if startswith and endswith { and } respectively
            List<string> list = filterString.TrimStart('{').TrimEnd('}').Split(',').ToList();

            // TODO: check if list.count == 4
            // TODO: check if if startswith and endswith [ and ] respectively
            string col = list[0].TrimStart('[').TrimEnd(']');
            string s1 = list[0].TrimStart('[').TrimEnd(']');
            string s2 = list[0].TrimStart('[').TrimEnd(']');
            string type = list[0].TrimStart('[').TrimEnd(']');

            // TODO: check if enum is valid
            return new Filter(col, s1, s2, (FilterType)Enum.Parse(typeof(FilterType), type));
        }
    }
}