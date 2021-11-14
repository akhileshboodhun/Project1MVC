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
    [AuthorizeEmployee(Roles = "Admin")]
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
            int pageNumber = ServicesHelper.SanitizePageNumber(fc["pageNumber"]);
            int pageSize = ServicesHelper.SanitizePageNumber(fc["pageSize"]);
            string sortBy = ServicesHelper.SanitizeSortBy<Equipment>(fc["sortBy"]);
            string sortOrder = ServicesHelper.SanitizeSortOrder(fc["sortOrder"]);

            string complexFilterString = fc["complexFilterString"] != null ? fc["complexFilterString"] : "";
            IList<Services.Filter> filters = Services.Filter.FromComplexString(complexFilterString);

            bool orFilters = (fc["orFilters"] != null && fc["orFilters"].ToString().ToLower() == "true") ? true : false;

            // TODO: perform this block inside EquipmentRepository 
            int equipmentsCount = equipmentService.GetCount();
            int pageCount = equipmentsCount / pageSize;
            pageCount = pageCount < 1 ? 1 : pageCount;
            pageNumber = (equipmentsCount - (pageNumber * pageSize)) <= 0 ? 1 : pageNumber;

            // TODO: pass in actual list of columns to be displayed
            IList<string> cols = new List<string>();
            IList<Equipment> equipments = equipmentService.GetPaginatedList(pageNumber, pageSize, cols, sortBy, sortOrder, filters, orFilters);

            ViewBag.filterCols = new List<string>(){ "Type", "Brand", "CurrentStockCount", "ReStockThreshold" };
            ViewBag.filterInputValues = Services.Filter.GetFieldsDictionaryFromFiltersList<Equipment>(filters);

            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.pageCount = pageCount;

            ViewBag.displayPrimaryColumn = false;
            ViewBag.displayCols = cols;

            ViewBag.sortBy = sortBy;
            ViewBag.sortOrder = sortOrder;
            ViewBag.nextSortOrders = ServicesHelper.GetNextSortParams<Equipment>(sortBy, sortOrder);
            
            return View(equipments);
        }

        // POST: Equipment/Details
        [HttpPost]
        public ActionResult Details(string id)
        {
            if (id != null && id.All(char.IsDigit))
            {
                var equipment = equipmentService.Get(id.ToInt());

                // TODO: if equipment is null, we need to display "not found" error in view

                ViewBag.displayPrimaryColumn = false;
                ViewBag.displayCols = ServicesHelper.GetColumns<Equipment>();
                return View(equipment);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //GET: Equipment/Add
        public ActionResult Add()
        {
            ViewBag.displayCols = ServicesHelper.GetColumns<Equipment>(false);
            return View(new Equipment(type: " ", brand: " ", model: " ", description: " "));
        }

        // POST: Equipment/Add
        [HttpPost]
        public ActionResult Add(FormCollection fc)
        {
            try
            {
                // TODO: validate fields
                string type = fc["[Type]"];
                string brand = fc["[Brand]"];
                string model = fc["[Model]"];
                string description = fc["[Description]"];
                int currentStockCount = fc["[CurrentStockCount]"].ToInt();
                int reStockThreshold = fc["[ReStockThreshold]"].ToInt();

                var equipment = new Equipment(0, type, brand, model, description, currentStockCount, reStockThreshold);
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
                var equipment = equipmentService.Get(fc["id"].ToInt());

                // TODO: if equipment is null, we need to display "not found" error in view

                ViewBag.displayCols = ServicesHelper.GetColumns<Equipment>(false);
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