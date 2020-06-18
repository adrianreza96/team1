using FeastFreedom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeastFreedom.Controllers
{
    public class UserController : Controller {
        // GET: User
        public ActionResult Index()  {
            ViewBag.UserList = "Users List";
            UserDBHandler handler = new UserDBHandler();
            ModelState.Clear();
            return View(handler.getUsers());
            // what is reference script libraries
        }
    }
}