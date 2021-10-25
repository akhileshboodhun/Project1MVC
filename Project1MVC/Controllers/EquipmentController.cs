using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.Models;
using Project1MVC.Services;
using Project1MVC.DAL;

namespace Project1MVC.Controllers
{
    [AuthorizeEmployee(Roles ="Admin")]
    public class EquipmentController : Controller
    {
        // GET: Equipments
        public ActionResult Index(string orderBy)
        {
            var equipments = EquipmentDAL.Instance.GetAll();

            if (orderBy == null)
            {
                return View(equipments);
            }
            else if (orderBy.ToLower() == "type")
            {
                equipments = equipments.OrderBy(i => i.Type).ToList();
            }
            else if (orderBy.ToLower() == "brand")
            {
                equipments = equipments.OrderBy(i => i.Brand).ToList();

            }
            else if (orderBy.ToLower() == "model")
            {
                equipments = equipments.OrderBy(i => i.Model).ToList();

            }
            else if (orderBy.Contains("currentstockcount"))
            {
                equipments = equipments.OrderBy(i => i.CurrentStockCount).ToList();

            }
            else if (orderBy.Contains("threshold"))
            {
                equipments = equipments.OrderBy(i => i.ReStockThreshold).ToList();
            }

            return View(equipments);
        }

        // GET: Equipment/Details/5
        //public ActionResult Details(string id)
        //{
        //    var db = InMemoryEquipments.GetInstance();
        //    var equipment = db.Get(Guid.Parse(id));
        //    return View(equipment);
        //}

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