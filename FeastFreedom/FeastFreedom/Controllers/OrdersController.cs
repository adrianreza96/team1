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
            return View(orders.ToList());
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
        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "ItemName");
            ViewBag.UserId = new SelectList(db.Users, Convert.ToInt32(Session["Id"]), (string)Session["Name"]);
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UserId,MenuId,Quantity,IsPaid,OrderDate,ShippingAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "ItemName", order.MenuId);
            ViewBag.UserId = new SelectList(db.Users, (string)Session["Id"], (string)Session["Name"], order.UserId);
            return View(order);
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

        [HttpPost]
        public ActionResult SendEmail()
        {
            MailMessage mailtext = new MailMessage("nguluangel@gmail.com", (string)Session["Email"]);
            mailtext.Subject = "FeastFreedom Order Confirmation";
            mailtext.Body = "message to be sent";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("nguluangel@gmail.com", "97namangels19");

            try
            {
                smtp.Send(mailtext);
                ViewData["Error"] = "Confirmation sent to year email";
                RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewData["Error"] = "Some Error";
            }
            RedirectToAction("Index");
        }
    }
}
