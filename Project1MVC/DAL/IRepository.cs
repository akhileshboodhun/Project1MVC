using System.Collections.Generic;
using Project1MVC.Models;

namespace Project1MVC.DAL
{
    public interface IRepository<T>
    {
        bool Add(T obj);

        bool Delete(int id);

        T Get(int id);

        T Get(int id, IList<string> cols);

        IList<T> GetAll();

        IList<T> GetPaginatedList(IList<string> cols, int? pageNumber, int? pageSize, string sortBy, string sortOrder);

        int GetCount();

        bool Update(T obj);
    }
}