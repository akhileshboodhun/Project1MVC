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
        }

        // GET: Dashboard
        public JsonResult Tiles()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    _tiles.Add(new Tile(label: "Users", icon: "fa-user", desc: "Create, Edit, View and Delete Users", location: "/Users"));
                    _tiles.Add(new Tile(label: "Equipment", icon: "fa-laptop", desc: "Create, Edit, View and Delete Equipment", location: "/Equipment"));
                    _tiles.Add(new Tile(label: "Role", icon: "fa-id-badge", desc: "Create, Edit, View and Delete Roles", location: "/UserRoles"));
                    _tiles.Add(new Tile(label: "Supplier", icon: "fa-truck", desc: "Create, Edit, View and Delete Suppliers", location: "/Supplier"));
                    _tiles.Add(new Tile(label: "Order", icon: "fa-shopping-cart", desc: "Create, Edit, View and Delete Orders", location: "/Order"));
                }
                if (User.IsInRole("Admin") || User.IsInRole("Technician"))
                {
                    var temp = new List<Tile>();
                    temp.Add(new Tile(label: "Employees", icon: "fa-users", desc: "Create, Edit, View and Delete Employees", location: "/Employees"));
                    temp.AddRange(_tiles);
                    _tiles = temp;
                    _tiles.Add(new Tile(label: "Current Stock Report", icon: "fa-tasks", desc: "View current stock report for equipments", location: "/EquipmentsInStock"));
                    _tiles.Add(new Tile(label: "Future Stock Report", icon: "fa-hourglass-3", desc: "View future stock report for equipments", location: "/FutureStock"));
                    _tiles.Add(new Tile(label: "Assigned Equipments Report", icon: "fa-laptop", desc: "View assigned equipments report for employees", location: "/EquipmentAssignment/ViewAssignedEquipments"));

                }
            }

            return Json(_tiles, JsonRequestBehavior.AllowGet);
        }
    }
}