using Project1MVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class FutureStockController : Controller
    {
        // GET: FutureStock
        public ActionResult Index()
        {
            var futureStockDB = new FutureStockDAL();
            var futureStockList = futureStockDB.GetAll().ToList();
            return View(futureStockList);
        }

        // GET: FutureStock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FutureStock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FutureStock/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FutureStock/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FutureStock/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FutureStock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FutureStock/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
