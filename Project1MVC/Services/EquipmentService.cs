using System;
using System.Collections.Generic;
using System.Linq;
using Project1MVC.DAL;
using Project1MVC.Models;
using System.Web;
using System.Reflection;
//using System.Reflection;

namespace Project1MVC.Services
{
    public class EquipmentService : IEquipmentService
    {
        readonly IRepository<Equipment> equipmentRepo;

        public EquipmentService(IRepository<Equipment> repository)
        {
            equipmentRepo = repository;
        }

        public bool Add(Equipment obj)
        {
            return equipmentRepo.Add(obj);
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
            return equipmentRepo.Delete(id);
        }

        public int GetCount()
        {
            return equipmentRepo.GetCount();
        }

        public Equipment Get(int id)
        {
            return equipmentRepo.Get(id);
        }

        public IList<Equipment> GetAll()
        {
            return equipmentRepo.GetAll();
        }

        public IList<Equipment> GetPaginatedList(int? pageNumber, int? pageSize, string sortBy, string sortOrder)
        {
            int _pageNumber = pageNumber ?? 1;
            int _pageSize = pageSize ?? ServicesHelper.DefaultPageSize;
            string _sortBy = ServicesHelper.SanitizeSortBy<Equipment>(sortBy);
            string _sortOrder = ServicesHelper.SanitizeSortOrder(sortOrder);

            return equipmentRepo.GetPaginatedList(_pageNumber, _pageSize, _sortBy, _sortOrder);
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepo.Update(obj);
        }
    }
}