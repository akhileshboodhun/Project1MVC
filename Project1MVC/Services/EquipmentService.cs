using System;
using System.Collections.Generic;
using Project1MVC.DAL;
using Project1MVC.Models;

namespace Project1MVC.Services
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository equipmentRepository;

        public EquipmentService(IEquipmentRepository repository)
        {
            equipmentRepository = repository;
        }

        public bool Add(Equipment obj)
        {
            return equipmentRepository.Add(obj);
        }

        public void ReturnFromEmployee(int equipId, int empId)
        {
            throw new NotImplementedException();
        }

        public bool AssignToEmployee(int equipId, int empId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            return equipmentRepository.Delete(id);
        }

        public Equipment Get(int id)
        {
            return equipmentRepository.Get(id);
        }

        public IList<Equipment> GetAll()
        {
            return equipmentRepository.GetAll();
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepository.Update(obj);
        }
    }
}