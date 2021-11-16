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

        public int GetCount(IList<Filter> filters = null, bool orFilters = true)
        {
            return equipmentRepo.GetCount(filters, orFilters);
        }

        public Equipment Get(int id)
        {
            return equipmentRepo.Get(id);
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

        public IList<Equipment> GetPaginatedList(out PaginatedListInfo<Equipment> paginatedListInfo, out FilteringInfo<Equipment> filteringInfo, string pageNumber, string pageSize, string sortBy, string sortOrder, string complexFilterString, string orFilters)
        {
            // TODO: pass in actual list of columns to be displayed
            IList<string> displayCols = new List<string>() { "Type", "Brand", "Description", "CurrentStockCount", "ReStockThreshol" };
            IList<string> filterCols = new List<string>() { "Type", "Brand", "CurrentStockCount", "ReStockThreshold" };

            FilteringInfo<Equipment> filInfo = new FilteringInfo<Equipment>(filterCols, complexFilterString, orFilters);
            PaginatedListInfo<Equipment> pgInfo;
            IList<Equipment> list = equipmentRepo.GetPaginatedList(out pgInfo, displayCols, pageNumber, pageSize, sortBy, sortOrder, filInfo.Filters, filInfo.OrFilters);

            paginatedListInfo = pgInfo;
            filteringInfo = filInfo;
            return list;        
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepo.Update(obj);
        }
    }
}