using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class FilteringInfo<T>
    {
        public FilteringInfo(IList<string> filterCols, string complexFilterString, string orFilters)
        {
            IList<string> _filterCols = ServicesHelper.SanitizeColumns<T>(filterCols);
            string _complexFilterString = ServicesHelper.SanitizeString(complexFilterString);
            bool _orFilters = ServicesHelper.SanitizeBoolean(orFilters);

            IList<Filter> _filters = Filter.FromComplexString(_complexFilterString);
            IDictionary<string, string> _filterValues = Filter.GetFieldsDictionaryFromFiltersList<T>(_filters);

            Columns = _filterCols;
            FilterValues = _filterValues;
            OrFilters = _orFilters;
            Filters = _filters;
        }

        public IList<string> Columns
        {
            get; private set;
        }

        public IDictionary<string, string> FilterValues
        {
            get; private set;
        }

        public bool OrFilters
        {
            get; private set;
        }

        public IList<Filter> Filters
        {
            get; private set;
        }
    }
}