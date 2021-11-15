﻿using System;
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

        public IList<Equipment> GetPaginatedList(out int recordsCount, out int pageCount, out int adjustedPageNumber, int pageNumber, int pageSize, IList<string> cols = null, string sortBy = "", string sortOrder = "", IList<Filter> filters = null, bool orFilters = true)
        {
            int _pageNumber = ServicesHelper.SanitizePageNumber(pageNumber.ToString());
            int _pageSize = ServicesHelper.SanitizePageSize(pageSize.ToString());

            string _sortBy = ServicesHelper.SanitizeSortBy<Equipment>(sortBy);
            string _sortOrder = ServicesHelper.SanitizeSortOrder(sortOrder);

            IList<string> _cols = cols ?? ServicesHelper.GetColumns<Equipment>();
            _cols = ServicesHelper.SanitizeColumns<Equipment>(_cols);

            int _recordsCount = 0;
            int _pageCount = 0;
            int _adjustedPageNumber = 1;
            IList<Equipment> list = equipmentRepo.GetPaginatedList(out _recordsCount, out _pageCount, out _adjustedPageNumber, _cols, _pageNumber, _pageSize, _sortBy, _sortOrder, filters, orFilters);

            recordsCount = _recordsCount;
            pageCount = _pageCount;
            adjustedPageNumber = _adjustedPageNumber;
            return list;        
        }

        public bool Update(Equipment obj)
        {
            return equipmentRepo.Update(obj);
        }
    }
}