using System;
using System.Collections.Generic;
using System.Linq;
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
            IList<string> cols = new List<string>() { "Type", "Brand", "Model", "Description", "ReStockThreshold" };
            return equipmentRepo.Add(obj, cols);
        }

        public bool TakeFromEmployee(string equipId, string empId)
        {
            return false;
        }

        public bool AssignToEmployee(string equipId, string empId, string assignorId)
        {
            //Equipment equipment = equipmentRepo.Get(equipId, new List<string>() 
            //{ "CurrentStockCount" });

            bool status = false;

            //if (equipment.CurrentStockCount > 0)
            //{
            //    // start trasaction
            //    // 1.  Decrease currentstockcount
            //    // 2. add an entry in EquipmentEmployee
            //}

            return status;
        }

        public int GetCount(IList<Filter> filters = null, bool orFilters = true)
        {
            return equipmentRepo.GetCount(filters, orFilters);
        }

        public Equipment Get(string id)
        {
            IList<string> cols = ServicesHelper.GetColumns<Equipment>(true, false);
            return Get(id, cols);
        }

        public Equipment Get(string id, IList<string> cols)
        {
            Equipment obj = (id != null && id.All(char.IsDigit)) ? new Equipment(id.ToInt()) : null;
            return (obj != null) ? equipmentRepo.Get(obj, cols) : null;
        }

        public IList<Equipment> GetAll(IList<string> cols)
        {
            return equipmentRepo.GetAll(cols);
        }

        public IList<Equipment> GetPaginatedList(out PaginatedListInfo<Equipment> paginatedListInfo, out FilteringInfo<Equipment> filteringInfo, string pageNumber, string pageSize, string sortBy, string sortOrder, string complexFilterString, string orFilters)
        {
            IList<string> displayCols = new List<string>() { "Type", "Brand", "Description", "ReStockThreshold", "GrandTotal" };
            IList<string> filterCols = new List<string>() { "Type", "Brand", "ReStockThreshold" };

            FilteringInfo<Equipment> filInfo = new FilteringInfo<Equipment>(filterCols, complexFilterString, orFilters);
            PaginatedListInfo<Equipment> pgInfo;
            IList<Equipment> list = equipmentRepo.GetPaginatedList(out pgInfo, displayCols, pageNumber, pageSize, sortBy, sortOrder, filInfo.Filters, filInfo.OrFilters);

            paginatedListInfo = pgInfo;
            filteringInfo = filInfo;
            return list;
        }

        public bool Update(Equipment obj)
        {
            IList<string> cols = new List<string>() { "Type", "Brand", "Model", "Description", "ReStockThreshold" };
            return equipmentRepo.Update(obj, cols);
        }
    }
}