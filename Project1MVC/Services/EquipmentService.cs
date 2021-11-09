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

        public bool TakeFromEmployee(int equipId, int empId)
        {
            throw new NotImplementedException();
        }

        public bool AssignToEmployee(int equipId, int empId, int assignorId)
        {
            Equipment equipment = equipmentRepo.Get(equipId, new List<string>() 
            { "CurrentStockCount" });

            bool status = false;

            if (equipment.CurrentStockCount > 0)
            {
                // start trasaction
                // 1.  Decrease currentstockcount
                // 2. add an entry in EquipmentEmployee
            }

            return status;
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
            return this.Get(id, new List<string>());
        }

        public Equipment Get(int id, IList<String> cols)
        {
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);
            return equipmentRepo.Get(id, _cols);
        }

        public IList<Equipment> GetAll()
        {
            return equipmentRepo.GetAll();
        }

        public IList<Equipment> GetPaginatedList(IList<string> cols, int? pageNumber, int? pageSize, string sortBy, string sortOrder)
        {
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);
            int _pageNumber = pageNumber ?? 1;
            int _pageSize = pageSize ?? ServicesHelper.DefaultPageSize;
            string _sortBy = ServicesHelper.SanitizeSortBy<Equipment>(sortBy);
            string _sortOrder = ServicesHelper.SanitizeSortOrder(sortOrder);

            return equipmentRepo.GetPaginatedList(_cols, _pageNumber, _pageSize, _sortBy, _sortOrder);
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepo.Update(obj);
        }
    }
}