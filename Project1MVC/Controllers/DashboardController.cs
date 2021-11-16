using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class DashboardController : Controller
    {
        private IList<Tile> _tiles;
        public DashboardController()
        {
            _tiles = new List<Tile>();
            _tiles.Add(new Tile(label: "Users",icon: "",desc: "Mo User desc",location: "/Users"));
            _tiles.Add(new Tile(label: "Supplier",icon: "",desc: "Mo Supplier desc",location: "/Supplier"));
        }

        // GET: Dashboard
        public JsonResult Tiles()
        {
            return Json(_tiles, JsonRequestBehavior.AllowGet);
        }
    }
}