using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.Controllers
{
    [AuthorizeEmployee(Roles ="Admin")]
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult Index()
        {
            var db = InMemoryEquipments.GetInstance();
            var equipments = db.GetAll();
            return View(equipments);
        }

        // GET: Equipments/Details/5
        public ActionResult Details(string id)
        {
            var db = InMemoryEquipments.GetInstance();
            var equipment = db.Get(Guid.Parse(id));
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        [HttpPost]
        public ActionResult Create(string EquipmentName, int Quantity)
        {
            try
            {
                // TODO: Add insert logic here
                //if (ModelState.IsValid)
                //{

                var equipment = new Equipment(EquipmentName, Quantity);
                var db = InMemoryEquipments.GetInstance();
                db.Add(equipment);
                return RedirectToAction("Index");
                //}
                //return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(string id)
        {
            var db = InMemoryEquipments.GetInstance();
            var equipment = db.Get(Guid.Parse(id));
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, string EquipmentName, int Quantity)
        {
            try
            {
                // TODO: Add update logic here
                var db = InMemoryEquipments.GetInstance();
                var equipment = db.Get(Guid.Parse(id));
                equipment.EquipmentName = EquipmentName;
                equipment.Quantity = Quantity;
                db.Update(equipment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(string id)
        {
            var db = InMemoryEquipments.GetInstance();
            var equipment = db.Get(Guid.Parse(id));
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var db = InMemoryEquipments.GetInstance();
            var equipment = db.Get(Guid.Parse(id));
            try
            {
                // TODO: Add delete logic here
                db.Delete(equipment);
                return RedirectToAction("Index");
            }
            catch
            {

                return View(equipment);
            }
        }
    }
}
