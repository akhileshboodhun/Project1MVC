using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.Models;
using Project1MVC.Services;
using System.Reflection;

namespace Project1MVC.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees

        public PartialViewResult Assigned(IEnumerable<Equipment> equipments)
        {
            return PartialView("_AssignedPartialPage", equipments);
        }
        public ActionResult Index()
        {
            return View(Data.LstEmployees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            var employee = Data.LstEmployees.FirstOrDefault(el => el.Id == Guid.Parse(id));
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Equipments = Data.LstEquipments;
            List <SelectListItem> selectListItems = Data.LstEquipments.Select(el => new SelectListItem() { Value = el.EquipmentId.ToString(), Text = el.EquipmentName }).ToList() ;
            ViewBag.SelectListEquipments = selectListItems;
            ViewBag.tempEquipments = new List<Equipment>() { };
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(FormCollection fc, List<string> EquipmentId)
        {

            Console.WriteLine();
            try
            {
                // TODO: Add insert logic here
                var equipments_assigned = new List<Equipment>() { };
                EquipmentId.ForEach(id => 
                    equipments_assigned.Add(
                        Data.LstEquipments.FirstOrDefault(el => el.EquipmentId == Guid.Parse(id))
                    )
                );

                var employee = new Employee(fc["FirstName"],
                    fc["LastName"],
                    new Utilities().getDate(fc["DateOfBirth"], "yyyy-MM-dd"),
                    fc["Email"],
                    fc["Role"],
                    fc["EmployeeStatus"])
                { Equipments = equipments_assigned};
                Data.LstEmployees.Add(employee);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(fc["DateOfBirth"]);
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Equipments = Data.LstEquipments;
            List<SelectListItem> selectListItems = Data.LstEquipments.Select(el => new SelectListItem() { Value = el.EquipmentId.ToString(), Text = el.EquipmentName }).ToList();
            ViewBag.SelectListEquipments = selectListItems;
            ViewBag.tempEquipments = new List<Equipment>() { };
            var employee = Data.LstEmployees.FirstOrDefault(el => el.Id == Guid.Parse(id));
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection fc, List<string> EquipmentID)
        {
            try
            {
                // TODO: Add update logic here
                var equipments_assigned = new List<Equipment>() { };
                EquipmentID.ForEach(equipId =>
                    equipments_assigned.Add(
                        Data.LstEquipments.FirstOrDefault(el => el.EquipmentId == Guid.Parse(equipId))
                    )
                );

                var employee = Data.LstEmployees.FirstOrDefault(el => el.Id == Guid.Parse(id));
                var index = Data.LstEmployees.FindIndex(el => el.Id == employee.Id);
                employee.FirstName = fc["FirstName"];
                employee.LastName = fc["LastName"];
                employee.DateOfBirth = new Utilities().getDate(fc["DateOfBirth"], "yyyy-MM-dd");
                employee.Email = fc["Email"];
                employee.Role = fc["Role"];
                employee.EmployeeStatus = fc["EmployeeStatus"];
                employee.Equipments = equipments_assigned;

                Data.LstEmployees[index] = employee;

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            var employee = Data.LstEmployees.FirstOrDefault(el => el.Id == Guid.Parse(id));
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var employee = Data.LstEmployees.FirstOrDefault(el => el.Id == Guid.Parse(id));
                Data.LstEmployees.Remove(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
