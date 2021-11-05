using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project1MVC.Controllers
{
    public class AuthController : Controller
    {
        private static string GlobalReturnUrl { get; set; }
        // GET: Auth/Login
        public ActionResult Login(string returnUrl)
        {
            Employee employee = new Employee(0, "Admin", "Admin", DateTime.Now, "", "Admin", "Employed", null, "");

            if (returnUrl != null)
            {
                ModelState.AddModelError("", $"You need to be logged in to access {returnUrl}");
                GlobalReturnUrl = returnUrl;
            }
            return View(employee);
        }

        // POST: Auth/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
          
            // TODO: Add insert logic here
            //var emp_db = InMemoryEmployees.GetInstance();
            //var employees = emp_db.GetAll();
            //var employee = employees.FirstOrDefault(el => el.Email == Email && el.Password == Password);
            Employee employee = new Employee(1, "Admin", "Admin", DateTime.Now, email, "Admin", "Employed", null, password);
            if (password == "pass")
            {
                Session["EmployeeFirstName"] = employee?.FirstName;
                FormsAuthentication.SetAuthCookie(employee.Email, true);
                System.Diagnostics.Debug.WriteLine($"ReturnURL:{GlobalReturnUrl}");
                
                if (GlobalReturnUrl?.Length > 0)
                {
                    var returnUrl = GlobalReturnUrl;
                    GlobalReturnUrl = null;
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(employee);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
