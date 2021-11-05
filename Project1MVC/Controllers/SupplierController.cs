using Project1MVC.DAL;
using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Suppliers
        public ActionResult Index()
        {
            var suppliers = SupplierDAL.Instance.GetAll();
            return View(suppliers);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int id)
        {
            var supplier = SupplierDAL.Instance.Get(id);
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            //try
            //{
            //    SupplierDAL.Instance.Add(supplier);

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            return View();
            //}
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id)
        {
            //var supplierDB = new SupplierDAL();
            //var supplier = supplierDB.Get(id);
            return View();
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier supplier)
        {
            //var supplierDB = new SupplierDAL();
            //try
            //{
            //    supplierDB.Update(supplier);

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    supplier = supplierDB.Get(id);
            return View();
            //}
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int id)
        {
            SupplierDAL.Instance.Delete(id);
            return View();
        }

        // POST: Suppliers/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection fc)
        //{
        //        var supplierDB = new SupplierDAL();
        //        var supplier = supplierDB.Get(id);
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        supplierDB.Delete(supplier);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View(supplier);
        //    }
        //}
    }
}