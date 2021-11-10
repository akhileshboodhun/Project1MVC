using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index() => NotFound();
        // GET: Error/BadRequest
        public ActionResult BadRequest() => PartialView("../Error/Index", new Error(400, "Bad Request"));
        // GET: Error/Unauthorized
        public ActionResult Unauthorized() => PartialView("../Error/Index", new Error(401, "Unauthorized"));
        // GET: Error/Forbidden
        public ActionResult Forbidden() => PartialView("../Error/Index", new Error(403, "Forbidden"));
        // GET: Error/NotFound
        public ActionResult NotFound() => PartialView("../Error/Index", new Error(404, "Not Found"));
        // GET: Error/ServerError
        public ActionResult ServerError() => PartialView("../Error/Index", new Error(500, "ServerError"));
    }
}