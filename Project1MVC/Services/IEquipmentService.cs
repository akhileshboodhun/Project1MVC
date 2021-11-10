using System.Collections.Generic;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public interface IEquipmentService
    {
        bool AssignToEmployee(int equipId, int empId, int assignorId);

        bool TakeFromEmployee(int equipId, int empId);

        bool Add(Equipment obj);

        Equipment Get(int id);

        Equipment Get(int id, IList<string> cols);

        IList<Equipment> GetAll();

        IList<Equipment> GetPaginatedList(int? pageNumber, int? pageSize, IList<string> cols = null, string sortBy = "", string sortOrder = "");

        bool Update(Equipment obj);
    }
}