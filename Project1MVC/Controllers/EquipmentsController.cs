using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.Models;

namespace Project1MVC.Controllers
{
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult Index()
        {
            var lstEquipments = Data.LstEquipments;
            return View(lstEquipments);
        }

        // GET: Equipments/Details/5
        public ActionResult Details(string id)
        {
            System.Diagnostics.Debug.WriteLine($"The id: {id}");
            var lstEquipments = Data.LstEquipments;

            var equipment = lstEquipments.FirstOrDefault<Equipment>(el => el.EquipmentId == new Guid(id));
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                //if (ModelState.IsValid)
                //{
                    Equipment equipment = new Equipment(collection["EquipmentName"]);
                    Data.LstEquipments.Add(equipment);
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
            var lstEquipments = Data.LstEquipments;

            var equipment = lstEquipments.FirstOrDefault<Equipment>(el => el.EquipmentId == new Guid(id));
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var lstEquipments = Data.LstEquipments;

                var equipment = lstEquipments.FirstOrDefault<Equipment>(el => el.EquipmentId == new Guid(id));
                var index = lstEquipments.FindIndex(e => e.EquipmentId == equipment.EquipmentId);
                equipment.EquipmentName = collection["EquipmentName"].ToString();
                lstEquipments[index] = equipment;
                Data.LstEquipments = lstEquipments;

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
            var lstEquipments = Data.LstEquipments;

            var equipment = lstEquipments.FirstOrDefault<Equipment>(el => el.EquipmentId == new Guid(id));
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var lstEquipments = Data.LstEquipments;

            var equipment = lstEquipments.FirstOrDefault<Equipment>(el => el.EquipmentId == new Guid(id));
            try
            {
                // TODO: Add delete logic here
                lstEquipments.Remove(equipment);
                return RedirectToAction("Index");
            }
            catch
            {

                return View(equipment);
            }
        }
    }
}
