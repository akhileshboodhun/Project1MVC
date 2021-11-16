using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.Services
{
    public class PaginatedListInfo<T>
    {
        public PaginatedListInfo(IList<string> cols, string pageNumber, string pageSize, int recordsCount, string sortBy, string sortOrder)
        {
            IList<string> _cols = ServicesHelper.SanitizeColumns<T>(cols);
            int _pageNumber = SanitizePageNumber(pageNumber);
            int _pageSize = SanitizePageSize(pageSize);
            string _sortBy = ServicesHelper.SanitizeSortBy<T>(sortBy);
            string _sortOrder = ServicesHelper.SanitizeSortOrder(sortOrder);

            int remainder;
            int pageCount = Math.DivRem(recordsCount, _pageSize, out remainder);
            pageCount = (remainder == 0) ? pageCount : pageCount + 1;
            pageCount = pageCount < 1 ? 1 : pageCount;

            int adjustedPageNumber =
                ((recordsCount - (_pageNumber * _pageSize)) < 0) && (_pageNumber != pageCount) ?
                1 : _pageNumber;

            this.PageNumber = adjustedPageNumber;
            this.PageSize = _pageSize;
            this.PageCount = pageCount;
            this.RecordsCount = recordsCount;
            this.SortBy = _sortBy;
            this.SortOrder = _sortOrder;
            this.DisplayColumns = _cols;
            this.DisplayPrimaryColumn = false;
        }

        public int PageNumber
        {
            get; private set;
        }

        public int PageSize
        {
            get; private set;
        }

        public int PageCount
        {
            get; private set;
        }

        public int RecordsCount
        {
            get; private set;
        }

        public string SortBy
        {
            get; private set;
        }

        public string SortOrder
        {
            get; private set;
        }

        public IDictionary<string, string> NextSortOrders
        {
            get
            {
                return ServicesHelper.GetNextSortParams<T>(SortBy, SortOrder);
            }
        }

        public bool DisplayPrimaryColumn
        {
            get; set;
        }

        public IList<string> DisplayColumns
        {
            // TODO: This setter needs to validate the list of cols supplied
            get; set;
        }

        public static int PageIncrement
        {
            get
            {
                return 3;
            }
        }

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
    }
}