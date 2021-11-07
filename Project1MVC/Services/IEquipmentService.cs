using System.Collections.Generic;
using Project1MVC.DAL;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public interface IEquipmentService : IRepository<Equipment>
    {
        bool AssignToEmployee(int equipId, int empId);

        void ReturnFromEmployee(int equipId, int empId);
    }
}