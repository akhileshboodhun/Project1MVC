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
    //[AuthorizeEmployee(Roles ="Admin")]
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult Index()
        {

            var equipmentDB = new EquipmentDAL();
            var equipments = equipmentDB.GetAll();
            return View(equipments);
        }

        // GET: Equipments/Details/5
        public ActionResult Details(int id)
        {
            var equipmentDB = new EquipmentDAL();
            var equipment = equipmentDB.Get(id);
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        [HttpPost]
        public ActionResult Create(Equipment equipment)
        {
            try
            {
                var equipmentDB = new EquipmentDAL();
                equipmentDB.Add(equipment);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int id)
        {
            var equipmentDB = new EquipmentDAL();
            var equipment = equipmentDB.Get(id);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Equipment equipment)
        {
            var equipmentDB = new EquipmentDAL();
            try
            {
                equipmentDB.Update(equipment);
                return RedirectToAction("Index");
            }
            catch
            {
                equipment = equipmentDB.Get(id);
                return View(equipment);
            }
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int id)
        {
            var equipmentDB = new EquipmentDAL();
            var equipment = equipmentDB.Get(id);
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var equipmentDB = new EquipmentDAL();
            var equipment = equipmentDB.Get(id);
            try
            {
                equipmentDB.Delete(equipment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(equipment);
               
            }
        }
    }
}
