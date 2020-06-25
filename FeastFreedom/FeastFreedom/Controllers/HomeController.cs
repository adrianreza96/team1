using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeastFreedom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            return View("Index");
        }

        public ActionResult Tester() {
            return View();
        }
    }
}