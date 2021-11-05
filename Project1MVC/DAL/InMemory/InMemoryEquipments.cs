using System;
using System.Collections.Generic;
using System.Linq;
using Project1MVC.DAL;
using Project1MVC.Models;

namespace Project1MVC.DAL
{
    public class InMemoryEquipments : IRepository<Equipment>
    {
        List<Equipment> equipments;

        private InMemoryEquipments()
        {
            equipments = new List<Equipment> {
            //new Equipment("Desktop 520"),
            //new Equipment("Laptop P1"),
            //new Equipment("Laptop Standard"),
            //new Equipment("Monitor"),
            //new Equipment("Mouse"),
            //new Equipment("Keyboard"),
            //new Equipment("Docking Station"),
            //new Equipment("headset")
        };
        }

        private static InMemoryEquipments _instance;

        public static InMemoryEquipments GetInstance()
        {
            if(_instance is null) _instance = new InMemoryEquipments();
            return _instance;
        }
        public bool Add(Equipment obj)
        {
            equipments.Add(obj);
            return true;
        }

        public bool Delete(int id)
        {
            var index = equipments.FindIndex(el => el.EquipId == id);
            equipments.RemoveAt(index);
            return true;
        }

        public Equipment Get(int id) => equipments.FirstOrDefault(el => el.EquipId == id);

        public IList<Equipment> GetAll() => equipments;

        public IList<Equipment> GetPaginatedList(int? pageNumber, int? pageSize, string sortBy, string sortOrder) => throw new NotImplementedException();

        public int GetCount() => equipments.Count;

        public bool Update(Equipment obj)
        {
            var index = equipments.FindIndex(el => el.EquipId == obj.EquipId);
            equipments[index] = obj;
            return true;
        }
    }
}