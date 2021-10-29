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
    [AuthorizeEmployee(Roles = "Admin")]
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
        public ActionResult Details(int id)
        {
            var equipment = EquipmentDAL.Instance.Get(id);
            //var equipment = db.Get(Guid.Parse(id));
            return View(equipment);
        }

        // GET: Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        public ActionResult Create(Equipment equipment)
        {
            var equipmentDB = EquipmentDAL.Instance;
            try
            {
                // TODO: Add insert logic here
                equipmentDB.Add(equipment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Equipment/Edit/5
        public ActionResult Edit(int id)
        {
            var equipmentDB = EquipmentDAL.Instance;
            var equipment = equipmentDB.Get(id);
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        //[HttpPost]
        [HttpPost]
        public ActionResult Edit(int id, Equipment equipment)
        {
            var equipmentDB = EquipmentDAL.Instance;

            try
            {
                // TODO: Add update logic here
                equipmentDB.Update(equipment);
                return RedirectToAction("Index");
            }
            catch
            {
                equipment = equipmentDB.Get(id);
                return View(equipment);
            }
        }

        // GET: Equipment/Delete/5
        public ActionResult Delete(int id)
        {
            var equipmentDB = EquipmentDAL.Instance;
            var equipment = equipmentDB.Get(id);
            return View(equipment);
        }

        // POST: Equipment/Delete/5
        //[HttpPost]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            var equipmentDB = EquipmentDAL.Instance;


            try
            {
                // TODO: Add delete logic here

                equipmentDB.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var equipment = equipmentDB.Get(id);
                return View(equipment);
            }
        }

    }


}