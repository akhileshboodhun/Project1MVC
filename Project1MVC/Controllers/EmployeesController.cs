using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1MVC.Models;
using Project1MVC.Services;
using System.Reflection;
using Project1MVC.DAL;

namespace Project1MVC.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees

        public PartialViewResult Assigned(IEnumerable<Equipment> equipments)
        {
            return PartialView("_AssignedPartialPage", equipments);
        }
        [AuthorizeUser(Roles = "Admin,Technician")]
        public ActionResult Index()
        {
            var empDB = EmployeeDAL.Instance;
            var employees = empDB.GetAll();
            return View(employees);
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var empDB = EmployeeDAL.Instance;
            var employee = empDB.Get(id);
            return View(employee);
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Employees/Create
        public ActionResult Create()
        {
            var userRoleDB = UserRoleDAL.Instance;

            var userRoles = userRoleDB.GetAll();
            List<SelectListItem> selectListItems = userRoles.Select(el => new SelectListItem() { Value = el.UserRoleId.ToString(), Text = el.RoleName }).ToList();
            var selectList = new SelectList(selectListItems, "Value", "Text");
            ViewBag.SelectListRoles = selectList;
            return View();
        }
        [AuthorizeUser(Roles = "Admin")]
        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee employee, List<int> EquipmentId)
        {

            var empDB = EmployeeDAL.Instance;
            try
            {
                // TODO: Add insert logic here
                var crypto = new CryptographyProcessor(size: 10);
                var salt = crypto.CreateSalt();
                var pwd = employee.HashedPassword;
                var storedHash = crypto.GenerateHash(pwd, salt);
                employee.Salt = salt;
                employee.HashedPassword = storedHash;

                empDB.Add(employee);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            var empDB = EmployeeDAL.Instance;
            var userRoleDB = UserRoleDAL.Instance;

            var userRoles = userRoleDB.GetAll();
            List<SelectListItem> selectListItems = userRoles.Select(el => new SelectListItem() { Value = el.UserRoleId.ToString(), Text = el.RoleName }).ToList();
            var selectList = new SelectList(selectListItems, "Value", "Text");
            ViewBag.SelectListRoles = selectList;

            var employee = empDB.Get(id);
            return View(employee);
        }

        [AuthorizeUser(Roles = "Admin")]
        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            var empDB = EmployeeDAL.Instance;
            try
            {
                // TODO: Add update logic here
                if (!String.IsNullOrEmpty(employee.HashedPassword))
                {
                    var crypto = new CryptographyProcessor(size: 10);
                    var salt = crypto.CreateSalt();
                    var pwd = employee.HashedPassword;
                    var storedHash = crypto.GenerateHash(pwd, salt);
                    employee.Salt = salt;
                    employee.HashedPassword = storedHash;
                }
                else
                {
                    var prevEmployee = empDB.Get(id);
                    employee.Salt = prevEmployee.Salt;
                    employee.HashedPassword = prevEmployee.HashedPassword;
                }
                empDB.Update(employee);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                employee = empDB.Get(id);
                return View(employee);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            var empDB = EmployeeDAL.Instance;
            var employee = empDB.Get(id);
            return View(employee);
        }

        [AuthorizeUser(Roles = "Admin")]
        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var empDB = EmployeeDAL.Instance;
            
            try
            {
                // TODO: Add delete logic here
                empDB.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var employee = empDB.Get(id);
                return View(employee);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Employees/Terminate/5
        public ActionResult Terminate(int id)
        {
            var empDB = EmployeeDAL.Instance;

            try
            {
                // TODO: Add delete logic here
                empDB.Terminate(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
