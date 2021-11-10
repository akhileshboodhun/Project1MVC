using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Project1MVC.Services
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authenticated
                base.HandleUnauthorizedRequest(filterContext);
            }
            else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {
                // The user is not in any of the listed roles => 
                // show the unauthorized view
                //filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized, "Oopss");
                filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Unauthorized" },
                                       { "controller", "Error" }
                                   });

            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}