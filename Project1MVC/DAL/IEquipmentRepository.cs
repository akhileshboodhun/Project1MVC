using System.Collections.Generic;
using Project1MVC.Models;

namespace Project1MVC.DAL
{
    public interface IEquipmentRepository
    {
        bool Add(Equipment obj);

        bool Delete(int id);

        Equipment Get(int id);

        IList<Equipment> GetAll();

        bool Update(Equipment obj);
    }
}