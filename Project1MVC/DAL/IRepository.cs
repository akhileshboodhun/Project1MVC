using System.Collections.Generic;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public interface IRepository<T>
    {
        bool Add(T obj);

        bool Delete(int id);

        T Get(int id);

        T Get(int id, IList<string> cols);

        IList<T> GetAll();

        IList<T> GetPaginatedList(out PaginatedListInfo<T> paginatedListInfo, IList<string> cols, int pageNumber, int pageSize, string sortBy, string sortOrder, IList<Filter> filters = null, bool orFilters = true);

        int GetCount(IList<Filter> filters = null, bool orFilters = true);

        bool Update(T obj);
    }
}