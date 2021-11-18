using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService equipmentService;

        public EquipmentController(IEquipmentService service)
        {
            equipmentService = service;
        }

        // GET: Equipments/Index
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(FormCollection fc)
        {
            PaginatedListInfo<Equipment> paginatedListInfo;
            FilteringInfo<Equipment> filteringInfo;
            IList<Equipment> equipments = equipmentService.GetPaginatedList(out paginatedListInfo, out filteringInfo, fc["pageNumber"], fc["pageSize"], fc["sortBy"], fc["sortOrder"], fc["complexFilterString"], fc["orFilters"]);

            ViewBag.paginatedListInfo = paginatedListInfo;
            ViewBag.filteringInfo = filteringInfo;

            return View(equipments);
        }

        // POST: Equipment/Details
        [HttpPost]
        public ActionResult Details(string id)
        {
            IList<string> cols = new List<string>() { "EquipId", "Type", "Brand", "Model", "Description" };
            var equipment = equipmentService.Get(id, cols);

            if (equipment != null)
            {
                ViewBag.displayPrimaryColumn = false;
                ViewBag.displayCols = cols;
                return View(equipment);
            }
            else
            {
                // TODO: show error message
                return RedirectToAction("Index");
            }
        }

        //GET: Equipment/Add
        public ActionResult Add()
        {
            IList<string> cols = new List<string>() { "Type", "Brand", "Model", "Description", "ReStockThreshold"};
            ViewBag.displayCols = cols;
            return View(new Equipment(type: " ", brand: " ", model: " ", description: " "));
        }

        // POST: Equipment/Add
        [HttpPost]
        public ActionResult Add(FormCollection fc)
        {
            try
            {
                string type = fc["[Type]"];
                string brand = fc["[Brand]"];
                string model = fc["[Model]"];
                string description = fc["[Description]"];
                int reStockThreshold = fc["[ReStockThreshold]"].ToInt();

                var equipment = new Equipment(0, type, brand, model, description, 0, reStockThreshold);
                equipmentService.Add(equipment);

                // TODO: check status of update and notify user instead of redirecting
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                return RedirectToAction("Index");
            }
        }

        //POST: Equipment/Edit
        [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
            if (fc["id"] != null && fc["id"].All(char.IsDigit))
            {
                var equipment = equipmentService.Get(fc["id"]);

                // TODO: if equipment is null, we need to display "not found" error in view

                IList<string> cols = new List<string>() { "Type", "Brand", "Model", "Description", "ReStockThreshold"};
                ViewBag.displayCols = cols;
                return View(equipment);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //POST: Equipment/Update
        [HttpPost]
        public ActionResult Update(FormCollection fc)
        {
            string primaryKey = "EquipId";

            if (fc[primaryKey] != null && fc[primaryKey].All(char.IsDigit))
            {
                try
                {
                    // TODO: validate fields other than primaryKey
                    // TODO: move this into service
                    int id = fc[primaryKey].ToInt();
                    string type = fc["[Type]"];
                    string brand = fc["[Brand]"];
                    string model = fc["[Model]"];
                    string description = fc["[Description]"];
                    int currentStockCount = fc["[CurrentStockCount]"].ToInt();
                    int reStockThreshold = fc["[ReStockThreshold]"].ToInt();

                    var equipment = new Equipment(id, type, brand, model, description, currentStockCount, reStockThreshold);
                    equipmentService.Update(equipment);

                    // TODO: check status of update and notify user instead of redirecting
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}