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
            var futureStockDB = FutureStockDAL.Instance;
            var futureStockList = futureStockDB.GetAll().ToList();
            double grandTotal = futureStockList.Sum(el => el.NetPrice);
            ViewBag.GrandTotal = grandTotal;
            return View(futureStockList);
        }
    }
}
