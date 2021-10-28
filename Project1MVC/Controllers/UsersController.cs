using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var userDB = UserDAL.Instance;
            var users = userDB.GetAll();
            var userRoleDB = UserRoleDAL.Instance;
            var userRoles = userRoleDB.GetAll();
            ViewBag.userRoles = userRoles;
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            var userDB = UserDAL.Instance;
            var user = userDB.Get(id);

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRoles = userRoleDB.GetAll();
            List<SelectListItem> selectListItems = userRoles.Select(el => new SelectListItem() { Value = el.UserRoleId.ToString(), Text = el.RoleName }).ToList();
            var selectList = new SelectList(selectListItems, "Value", "Text");
            ViewBag.SelectListRoles = selectList;
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                var userDB = UserDAL.Instance;
                var crypto = new CryptographyProcessor(size: 10);
                var salt = crypto.CreateSalt();
                var pwd = user.HashedPassword;
                var storedHash = crypto.GenerateHash(pwd, salt);
                user.Salt = salt;
                user.HashedPassword = storedHash;
                userDB.Add(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var userDB = UserDAL.Instance;
            var user = userDB.Get(id);

            var userRoleDB = UserRoleDAL.Instance;
            var userRoles = userRoleDB.GetAll();
            List<SelectListItem> selectListItems = userRoles.Select(el => new SelectListItem() { Value = el.UserRoleId.ToString(), Text = el.RoleName }).ToList();
            var selectList = new SelectList(selectListItems, "Value", "Text");
            ViewBag.SelectListRoles = selectList;

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var userDB = UserDAL.Instance;
            
            try
            {
                // TODO: Add update logic here
                if (!String.IsNullOrEmpty(user.HashedPassword))
                {
                var crypto = new CryptographyProcessor(size: 10);
                var salt = crypto.CreateSalt();
                var pwd = user.HashedPassword;
                var storedHash = crypto.GenerateHash(pwd, salt);
                user.Salt = salt;
                user.HashedPassword = storedHash;
                }
                else
                {
                    user.HashedPassword = userDB.Get(id).HashedPassword;
                }
                userDB.Update(user);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                user = userDB.Get(id);
                return View(user);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            var userDB = UserDAL.Instance;
            var user = userDB.Get(id);

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            var userDB = UserDAL.Instance;
            try
            {
                // TODO: Add delete logic here
                userDB.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                user = userDB.Get(id);
                return View(user);
            }
        }
    }
}
