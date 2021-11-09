using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.Controllers
{
    [AuthorizeEmployee(Roles = "Admin")]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService equipmentService;

        public EquipmentController(IEquipmentService service)
        {
            equipmentService = service;
        }

        // GET: Equipments
        [HttpGet]
        public ActionResult Index(int? pageNumber, int? pageSize, string sortBy = "EquipId", string sortOrder = "asc")
        {
            List<string> cols = new List<string>();// { "fff", "CurrentffCount" };
            var equipments = equipmentService.GetPaginatedList(cols, pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.nextSortOrders = ServicesHelper.GetNextSortParams<Equipment>(sortBy, sortOrder);
            ViewBag.displayCols = ServicesHelper.SanitizeColumns<Equipment>(cols);
            return View(equipments);
        }

        // POST: Equipment/Details
        [HttpPost]
        public ActionResult Details(string id)
        {
            if (id != null && id.All(char.IsDigit))
            {
                List<string> cols = new List<string>();
                var equipment = equipmentService.Get(id.ToInt(), cols);

                // TODO: if equipment is null, we need to display "not found" error in view

                ViewBag.displayCols = ServicesHelper.SanitizeColumns<Equipment>(cols);
                return View(equipment);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Equipment/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Equipment/Create
        //[HttpPost]
        //public ActionResult Create(string EquipmentName, int Quantity)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        //if (ModelState.IsValid)
        //        //{

        //        var equipment = new Equipment(EquipmentName, Quantity);
        //        var db = InMemoryEquipments.GetInstance();
        //        db.Add(equipment);
        //        return RedirectToAction("Index");
        //        //}
        //        //return View();

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Equipment/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    var db = InMemoryEquipments.GetInstance();
        //    var equipment = db.Get(Guid.Parse(id));
        //    return View(equipment);
        //}

        // POST: Equipment/Edit/5
        //[HttpPost]
        //public ActionResult Edit(string id, string EquipmentName, int Quantity)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        var db = InMemoryEquipments.GetInstance();
        //        //var equipment = db.Get(Guid.Parse(id));
        //        //equipment.EquipmentName = EquipmentName;
        //        //equipment.Quantity = Quantity;
        //        //db.Update(equipment);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Equipment/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    var db = InMemoryEquipments.GetInstance();
        //    var equipment = db.Get(Guid.Parse(id));
        //    return View(equipment);
        //}

        // POST: Equipment/Delete/5
        //[HttpPost]
        //public ActionResult Delete(string id, FormCollection collection)
        //{
        //    var db = InMemoryEquipments.GetInstance();
        //    var equipment = db.Get(Guid.Parse(id));
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        db.Delete(equipment);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {

        //        return View(equipment);
        //    }
        //}
    }
}