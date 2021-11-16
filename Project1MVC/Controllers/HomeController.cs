using Project1MVC.DAL;
using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 0)]
        public ActionResult Index()
        {
            //EquipmentDAL.Instance.Add(new Equipment(0, "Diskette", "Blala", "HP", "Floppy disk"));
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Auth");
            return View();
        }

        public ActionResult About()
        {
            //var supplierDB = new SupplierDAL();
            ////supplierDB.Add(new Supplier("Dharmesh", "58108134", "Riv Du Poste"));
            ////System.Diagnostics.Debug.WriteLine(supplier.Name);
            ////var supplierList = supplierDB.GetAll().ToList();
            //var supplier = supplierDB.Get(1);
            //supplier.Address = "Port Louis";
            //supplierDB.Update(supplier);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Equipment(Equipment equipment)
        {
            //ViewBag.Message = "Your contact page.";

            return View(equipment);
        }
    }
}