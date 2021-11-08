using System.Collections.Generic;
using Project1MVC.DAL;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public interface IEquipmentService : IRepository<Equipment>
    {
        bool AssignToEmployee(int equipId, int empId, int assignorId);

        bool TakeFromEmployee(int equipId, int empId);
    }
}