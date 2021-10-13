using Project1MVC.Models;
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
        public ActionResult Login(string ReturnUrl)
        {
            if (ReturnUrl != null) {
                ModelState.AddModelError("", $"You need to be logged in to access {ReturnUrl}");
                GlobalReturnUrl = ReturnUrl;
            }
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
                if (!(employee is null))
                {
                    Session["EmployeeFirstName"] = employee?.FirstName;
                    FormsAuthentication.SetAuthCookie(employee.Email, false);
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
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
