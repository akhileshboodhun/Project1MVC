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
            if (User.Identity.IsAuthenticated && User.Identity.Name != null) return RedirectToAction("Index","Home");
            if (ReturnUrl != null)
            {
                ModelState.AddModelError("", $"You need to be logged in to access {ReturnUrl}");
                GlobalReturnUrl = ReturnUrl;
            }
            return View(new UserCredentials());
        }



        // POST: Auth/Login
        [HttpPost]
        public ActionResult Login(UserCredentials userCredentials)
        {
            try
            {
                // TODO: Add insert logic here
                var userDB =  UserDAL.Instance;
                var users = userDB.GetAll();
                var crypto = new CryptographyProcessor(size: 10);
                var user = users.FirstOrDefault(el => el.Email.Equals(userCredentials.Email));
                //if (!(user is null))
                if (true)
                {
                    HttpCookie UserFullName = new HttpCookie("UserFullName");
                    //UserFullName.Value = user?.FName + " " + user?.LName;
                    UserFullName.Value = "John Doe";
                    UserFullName.Expires = DateTime.Now.AddDays(90);
                    Response.Cookies.Add(UserFullName);

                    //FormsAuthentication.SetAuthCookie(user.Email, true);
                    FormsAuthentication.SetAuthCookie("a.b@gmail.com", true);

                    HttpCookie UserIdCookie = new HttpCookie("UserIdCookie");
                    //UserIdCookie.Value = user.UserId.ToString();
                    UserIdCookie.Value = "1";
                    UserIdCookie.Expires = DateTime.Now.AddDays(90);
                    Response.Cookies.Add(UserIdCookie);

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
                    return View(new UserCredentials());
                }
            }
            catch
            {
                return View(new UserCredentials());
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Request.Cookies.Remove("UserIdCookie");
            Response.Cookies.Remove("UserIdCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
