using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class InMemoryEquipments : IManageEquipments
    {
        List<Equipment> lstEquipments;
        private InMemoryEquipments()
        {
            lstEquipments = new List<Equipment> {
            new Equipment("Desktop 520"),
            new Equipment("Laptop P1"),
            new Equipment("Laptop Standard"),
            new Equipment("Monitor"),
            new Equipment("Mouse"),
            new Equipment("Keyboard"),
            new Equipment("Docking Station"),
            new Equipment("headset")
        };
        }

        private static InMemoryEquipments _instance;

        public static InMemoryEquipments GetInstance() => _instance ?? new InMemoryEquipments();
        public void Add(Equipment obj) => lstEquipments.Add(obj);

        public void Delete(Equipment obj) => lstEquipments.Remove(obj);

        public Equipment Get(Guid id) => lstEquipments.FirstOrDefault(el => el.EquipmentId == id);

        public IEnumerable<Equipment> GetAll() => lstEquipments;


        public void Update(Equipment obj)
        {
            var index = lstEquipments.FindIndex(el => el.EquipmentId == obj.EquipmentId);
            lstEquipments[index] = obj;
        }
    }
}