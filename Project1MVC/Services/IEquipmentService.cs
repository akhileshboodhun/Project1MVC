using System.Collections.Generic;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public interface IEquipmentService
    {
        bool AssignToEmployee(string equipId, string empId, string assignorId);

        bool TakeFromEmployee(string equipId, string empId);

        bool Add(Equipment obj);

        int GetCount(IList<Filter> filters = null, bool orFilters = true);

        Equipment Get(string id);

        Equipment Get(string id, IList<string> cols);

        IList<Equipment> GetAll(IList<string> cols);

        IList<Equipment> GetPaginatedList(out PaginatedListInfo<Equipment> paginatedListInfo, out FilteringInfo<Equipment> filteringInfo, string pageNumber, string pageSize, string sortBy, string sortOrder, string complexFilterString, string orFilters);

        bool Update(Equipment obj);
    }
}