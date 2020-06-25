using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;

namespace FeastFreedom.Controllers
{
    public class OrdersController : Controller
    {
        private feastfreedomEntities db = new feastfreedomEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Menu).Include(o => o.User);

            ViewData["Success"] = TempData["Success"];
            ViewData["Error"] = TempData["Error"];
            return View(orders.ToList());
        }

        public ActionResult Tester() {
            if (Session["cart"] != null) {
                List<Menu> items = Session["cart"] as List<Menu>;
                return View(items);
            }
            return View();
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);  //userid and isPaid
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create() {        
            if (Session["Id"]==null) {
                Session["last"] = "Create";
                return RedirectToAction("Login", "Users");
            }
            else {  // user authenticated
                int id = Int32.Parse(Session["Id"].ToString());
                IEnumerable<User> users = (from u in db.Users where u.UserId == id select u).ToList<User>();
                ViewBag.user = users.First();
                return View();
            }
            //return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "OrderId,UserId,MenuId,Quantity,IsPaid,OrderDate,ShippingAddress")] Order order) {
        public ActionResult Create([Bind(Include = "OrderDate,ShippingAddress")] Order order) {
                int id = Int32.Parse(Session["Id"].ToString()); ;
            IEnumerable<User> users = (from u in db.Users where u.UserId == id select u).ToList<User>();
            ViewBag.user = users.First();
            if (ModelState.IsValid)
            {
                order.UserId = id;
                db.Orders.Add(order);
                db.SaveChanges();
                Session["cart"] = null;
                Session["count"] = "";
                Session["last"] = "checkedout";
                return RedirectToAction("SendEmail");
            }
            return View(order);
        }

        public ActionResult OrderConfirmed() {
            return View();
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "ItemName", order.MenuId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,MenuId,Quantity,IsPaid,OrderDate,ShippingAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "ItemName", order.MenuId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public ActionResult SendEmail()
        {
            MailMessage mailtext = new MailMessage("feastfreed@gmail.com", "nguluangel@gmail.com");
            mailtext.Subject = "FeastFreedom Order Confirmation";
            mailtext.Body = "message to be sent";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("feastfreed@gmail.com", "@feastfreedom");

            try
            {
                smtp.Send(mailtext);
                TempData["Success"] = "Confirmation sent to your email";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Invalid Email Account";
            }
            return RedirectToAction("Index");
        }
    }
}
