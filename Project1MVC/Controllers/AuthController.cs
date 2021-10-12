using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }



        // POST: Auth/Login
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                // TODO: Add insert logic here
                var employee = Data.LstEmployees.FirstOrDefault(el => el.Email == Email && el.Password == Password);
                Session["EmployeeFirstName"] = employee?.FirstName;
                
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auth/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auth/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
