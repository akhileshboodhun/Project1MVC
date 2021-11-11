using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class OrderController : Controller
    {

        private OrderDAL orderDB;
        private EquipmentDAL equipmentDB;
        private SupplierDAL supplierDB;

        public OrderController()
        {
            orderDB = OrderDAL.Instance;
            equipmentDB = EquipmentDAL.Instance;
            supplierDB = SupplierDAL.Instance;
        }
        // GET: Order
        public ActionResult Index()
        {
            //var orderDB = OrderDAL.Instance;
            var orderWrapper = orderDB.GetAll();
            return View(orderWrapper);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {

            //var orderDB = OrderDAL.Instance;
            var orderWrapper = orderDB.Get(id);
            return View(orderWrapper);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            //EquipmentDAL equipmentDB = EquipmentDAL.Instance;
            var equipments = equipmentDB.GetAll();
            //SupplierDAL supplierDB = SupplierDAL.Instance;
            var suppliers = supplierDB.GetAll();
            ViewBag.EquipmentsList = equipments;
            ViewBag.SuppliersList = suppliers;
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order, EquipmentOrder[] equipmentOrder, FormCollection collection)
        {
            //var orderDB = OrderDAL.Instance;
            OrderWrapper orderWrapper = new OrderWrapper();
            orderWrapper.OrderProp = order;
            orderWrapper.EquipmentOrderProp = equipmentOrder;
            try
            {
                // TODO: Add insert logic here
                orderDB.Add(orderWrapper);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            //var orderDB = OrderDAL.Instance;
            var orderWrapper = orderDB.Get(id);
            //EquipmentDAL equipmentDB = EquipmentDAL.Instance;
            var equipments = equipmentDB.GetAll();
            //SupplierDAL supplierDB = SupplierDAL.Instance;
            var suppliers = supplierDB.GetAll();
            ViewBag.EquipmentsList = equipments;
            ViewBag.SuppliersList = suppliers;
            return View(orderWrapper);
        }

        [HttpGet]
        public ActionResult EditEquipment(int id, int equipmentIndex)
        {

            var orderWrapper = orderDB.Get(id);
            ViewBag.OrderId = id;
            return View(orderWrapper.EquipmentOrderProp[equipmentIndex]);
        }

        [HttpPost]
        public ActionResult EditEquipment(int id, int equipmentIndex, EquipmentOrder equipmentOrder)
        {
            try
            {
                // TODO: Add update logic here
                orderDB.UpdateEquipment(id, equipmentOrder);
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteEquipment(int id, int equipmentIndex)
        {

            var orderWrapper = orderDB.Get(id);
            ViewBag.OrderId = id;
            return View(orderWrapper.EquipmentOrderProp[equipmentIndex]);
        }

        [HttpPost]
        public ActionResult DeleteEquipment(int id, int equipmentIndex, FormCollection fc)
        {
            try
            {
                // TODO: Add update logic here
                var orderWrapper = orderDB.Get(id);
                var equipmentId = orderWrapper.EquipmentOrderProp[equipmentIndex].EquipmentId;
                orderDB.DeleteEquipment(id, equipmentId);
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrderWrapper orderWrapper)
        {
            try
            {
                // TODO: Add update logic here
                orderWrapper.OrderProp.Id = id;
                orderDB.Update(orderWrapper);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var orderDB = OrderDAL.Instance;
            var orderWrapper = orderDB.Get(id);
            return View(orderWrapper.OrderProp);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var orderDB = OrderDAL.Instance;
            try
            {
                // TODO: Add delete logic here

                orderDB.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var order = orderDB.Get(id);
                return View(order);
            }
        }
    }
}
