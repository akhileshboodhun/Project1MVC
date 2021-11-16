using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class PaginatedListInfo<T>
    {
        public PaginatedListInfo(int pageNumber, int pageSize, int pageCount, int recordsCount, string sortBy, string sortOrder)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.PageCount = pageCount;
            this.RecordsCount = recordsCount;
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
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