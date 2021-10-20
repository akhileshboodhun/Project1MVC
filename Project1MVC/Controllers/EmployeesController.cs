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
    [AuthorizeEmployee(Roles = "Admin,Technician")]
    public class EmployeesController : Controller
    {
        // GET: Employees

    /*    public PartialViewResult Assigned(IEnumerable<Equipment> equipments)
        {
            return PartialView("_AssignedPartialPage", equipments);
        }
        public ActionResult Index()
        {
            var db = InMemoryEmployees.GetInstance();
            var employees = db.GetAll().ToList();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            var db = InMemoryEmployees.GetInstance();
            var employee = db.Get(Guid.Parse(id));
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var db = InMemoryEquipments.GetInstance();
            var equipments = db.GetAll().ToList();
            ViewBag.Equipments = equipments;
            List <SelectListItem> selectListItems = equipments.Select(el => new SelectListItem() { Value = el.EquipmentId.ToString(), Text = el.EquipmentName }).ToList() ;
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
                var db = InMemoryEquipments.GetInstance();
                var equipments = db.GetAll().ToList();

                var equipments_assigned = new List<Equipment>() { };
                EquipmentId.ForEach(id => 
                    equipments_assigned.Add(
                        db.Get(Guid.Parse(id)))
                );

                var employee = new Employee(fc["FirstName"],
                    fc["LastName"],
                    new Utilities().getDate(fc["DateOfBirth"], "yyyy-MM-dd"),
                    fc["Email"],
                    fc["Role"],
                    fc["EmployeeStatus"])
                { Equipments = equipments_assigned};
                var emp_db = InMemoryEmployees.GetInstance();
                emp_db.Add(employee);
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
            var db = InMemoryEquipments.GetInstance();
            var emp_db = InMemoryEmployees.GetInstance();
            var equipments = db.GetAll().ToList();
            ViewBag.Equipments = equipments;
            List<SelectListItem> selectListItems = equipments.Select(el => new SelectListItem() { Value = el.EquipmentId.ToString(), Text = el.EquipmentName }).ToList();
            ViewBag.SelectListEquipments = selectListItems;
            ViewBag.tempEquipments = new List<Equipment>() { };
            var employee = emp_db.Get(Guid.Parse(id));
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection fc, List<string> EquipmentID)
        {
            try
            {
                // TODO: Add update logic here
                var db = InMemoryEquipments.GetInstance();
                var emp_db = InMemoryEmployees.GetInstance();
                var equipments = db.GetAll().ToList();
                var equipments_assigned = new List<Equipment>() { };
                EquipmentID.ForEach(equipId =>
                    equipments_assigned.Add(
                        db.Get(Guid.Parse(equipId))
                    )
                );

                var employee = emp_db.Get(Guid.Parse(id));
                employee.FirstName = fc["FirstName"];
                employee.LastName = fc["LastName"];
                employee.DateOfBirth = new Utilities().getDate(fc["DateOfBirth"], "yyyy-MM-dd");
                employee.Email = fc["Email"];
                employee.Role = fc["Role"];
                employee.EmployeeStatus = fc["EmployeeStatus"];
                employee.Equipments = equipments_assigned;

                emp_db.Update(employee);

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
            var emp_db = InMemoryEmployees.GetInstance();
            var employee = emp_db.Get(Guid.Parse(id));
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var emp_db = InMemoryEmployees.GetInstance();
                var employee = emp_db.Get(Guid.Parse(id));
                emp_db.Delete(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } */
    }
}
