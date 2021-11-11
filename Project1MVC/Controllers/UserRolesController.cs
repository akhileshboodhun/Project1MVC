using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        // GET: UserRoles
        public ActionResult Index()
        {

            var userRoleDB = UserRoleDAL.Instance;
            var userRoles = userRoleDB.GetAll().ToList();
            return View(userRoles);
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int id)
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRole = userRoleDB.Get(id);
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        [HttpPost]
        public ActionResult Create(UserRole userRole)
        {
            var userRoleDB = UserRoleDAL.Instance;
            try
            {
                // TODO: Add insert logic here
                userRoleDB.Add(userRole);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int id)
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRole = userRoleDB.Get(id);
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserRole userRole)
        {
            var userRoleDB = UserRoleDAL.Instance;
           
            try
            {
                // TODO: Add update logic here
                userRoleDB.Update(userRole);
                return RedirectToAction("Index");
            }
            catch
            {
                userRole = userRoleDB.Get(id);
                return View(userRole);
            }
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int id)
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRole = userRoleDB.Get(id);
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            var userRoleDB = UserRoleDAL.Instance;
            

            try
            {
                // TODO: Add delete logic here
                
                userRoleDB.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var userRole = userRoleDB.Get(id);
                return View(userRole);
            }
        }
    }
}
