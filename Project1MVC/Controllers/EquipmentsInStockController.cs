using Project1MVC.DAL;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    [AuthorizeUser(Roles ="Admin,Technician")]
    public class EquipmentsInStockController : Controller
    {
        // GET: EquipmentsInStock
        public ActionResult Index()
        {
            var equipmentsDB = EquipmentDAL.Instance;
            var equipmentsInStockList = equipmentsDB.GetAllEquipmentsInStock();

            return View(equipmentsInStockList);
        }
    }
}