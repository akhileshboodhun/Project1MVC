using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.Services
{
    public class PaginatedListInfo<T>
    {
        public PaginatedListInfo(string pageNumber, string pageSize, int recordsCount, string sortBy, string sortOrder)
        {
            int _pageNumber = ServicesHelper.SanitizePageNumber(pageNumber);
            int _pageSize = ServicesHelper.SanitizePageSize(pageSize);
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
            this.DisplayPrimaryColumn = false;
            this.DisplayColumns = new List<string>();
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
            get; set;
        }
    }
}