using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;

namespace FeastFreedom.Controllers
{
    public class UsersController : Controller
    {
        private feastfreedomEntities db = new feastfreedomEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.RoleId == 2 || x.RoleId == 3), "RoleId", "Role1");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Email,Password,ConfirmPassword,BillingAddress,SecurityQuestionOne,SecurityQuestionTwo,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {               
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Role1", user.RoleId);
            return View(user);
        }

        [HttpGet]
        public ActionResult Login() {           
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password) {
            IEnumerable<User> users = (from u in db.Users where u.Email == email select u).ToList<User>();
            if (users.Count() == 0) {
                ViewBag.error = "No user to email " + email;
                return View();
            }
            if (users.Count() == 1) {
                
                if (users.First().Password == password) {
                    Session["Id"] = users.First().UserId;
                    Session["Email"] = users.First().Email;
                    Session["Name"] = users.First().FirstName;
                    Session["Role"] = users.First().RoleId;
                    ViewBag.Users = users.First();
                    if (Session["cart"] != null) {
                        if (Session["last"] != null) {
                            return RedirectToAction(Session["last"].ToString(), "Orders");
                        }                        
                    }
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ViewBag.error = "Invalid user credentials.  Please retry ";
                    return View();
                }              
            }
            else {
                ViewBag.error = "Too many users retrieved";
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Role1", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Email,Password,BillingAddress,SecurityQuestionOne,SecurityQuestionTwo,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Role1", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Unauthorized User";

            return View();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
