using Project1MVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class EquipmentsInStockController : Controller
    {
        // GET: EquipmentsInStock
        public ActionResult Index()
        {
            var equipmentsDB = new EquipmentDAL();
            var equipmentsInStockList = equipmentsDB.GetAllEquipmentsInStock().ToList();
            return View(equipmentsInStockList);
        }
    }
}