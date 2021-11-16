using System.Collections.Generic;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public interface IEquipmentService
    {
        bool AssignToEmployee(int equipId, int empId, int assignorId);

        bool TakeFromEmployee(int equipId, int empId);

        bool Add(Equipment obj);

        int GetCount(IList<Filter> filters = null, bool orFilters = true);

        Equipment Get(int id);

        Equipment Get(int id, IList<string> cols);

        IList<Equipment> GetAll();

        IList<Equipment> GetPaginatedList(out PaginatedListInfo<Equipment> paginatedListInfo, out FilteringInfo<Equipment> filteringInfo, string pageNumber, string pageSize, string sortBy, string sortOrder, string complexFilterString, string orFilters);

        bool Update(Equipment obj);
    }
}