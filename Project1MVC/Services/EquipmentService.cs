using System;
using System.Collections.Generic;
using Project1MVC.DAL;
using Project1MVC.Models;

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

        public int GetCount()
        {
            return equipmentRepo.GetCount();
        }

        public Equipment Get(int id)
        {
            return this.Get(id, new List<string>());
        }

        public Equipment Get(int id, IList<string> cols)
        {
            IList<string> _cols = ServicesHelper.SanitizeColumns<Equipment>(cols);
            return equipmentRepo.Get(id, _cols);
        }

        public IList<Equipment> GetAll()
        {
            return equipmentRepo.GetAll();
        }

        public IList<Equipment> GetPaginatedList(int? pageNumber, int? pageSize, IList<string> cols = null, string sortBy = "", string sortOrder = "")
        {
            int _pageNumber = pageNumber ?? 1;
            _pageNumber = _pageNumber > 0 ? _pageNumber : 1;

            int _pageSize = pageSize ?? ServicesHelper.DefaultPageSize;
            _pageSize = _pageSize > 0 ? _pageSize : ServicesHelper.DefaultPageSize;

            string _sortBy = ServicesHelper.SanitizeSortBy<Equipment>(sortBy);
            string _sortOrder = ServicesHelper.SanitizeSortOrder(sortOrder);

            IList<string> _cols = cols ?? ServicesHelper.GetColumns<Equipment>();
            _cols = ServicesHelper.SanitizeColumns<Equipment>(_cols);

            return equipmentRepo.GetPaginatedList(_cols, _pageNumber, _pageSize, _sortBy, _sortOrder);
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepo.Update(obj);
        }
    }
}