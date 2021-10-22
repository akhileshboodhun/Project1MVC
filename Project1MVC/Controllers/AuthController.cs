using Project1MVC.DAL;
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
                var userDB = new UserDAL();
                var users = userDB.GetAll();
                var crypto = new CryptographyProcessor(size: 5);
                var user = users.FirstOrDefault(el => el.Email.Equals(Email) && crypto.AreEqual(plainTextInput: Password, saltedHashInput: el.HashedPassword));
                if (!(user is null))
                {
                    Session["UserFirstName"] = user?.FName;
                    FormsAuthentication.SetAuthCookie(user.Email, true);
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
