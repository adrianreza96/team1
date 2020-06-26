using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeastFreedom.Models
{
  
    public class AuthorizeFilter : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        private feastfreedomEntities db = new feastfreedomEntities();

        public AuthorizeFilter(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["UserId"]);
            if (!string.IsNullOrEmpty(userId))
            {

                User user = db.Users.Find(userId);

                foreach (var role in allowedroles)
                {
                    if (role == user.Role.Role1) return true;
                }
            }
                
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                { "controller", "Users" },
                { "action", "UnAuthorized" }
                });
        }
    }
   
}