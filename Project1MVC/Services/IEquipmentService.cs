using System.Collections.Generic;
using Project1MVC.DAL;

namespace Project1MVC.Services
{
    public interface IEquipmentService : IEquipmentRepository
    {
        bool AssignToEmployee(int equipId, int empId);

        void ReturnFromEmployee(int equipId, int empId);
    }
}