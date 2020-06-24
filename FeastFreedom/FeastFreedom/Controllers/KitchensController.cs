using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;

namespace FeastFreedom.Controllers
{
    public class KitchensController : Controller
    {
        private feastfreedomEntities db = new feastfreedomEntities();

        // GET: Kitchens
        public ActionResult Index()
        {
            var kitchens = db.Kitchens.Include(k => k.User);
            return View(kitchens.ToList());
        }

        public ActionResult List()
        {
            var kitchens = db.Kitchens.Include(k => k.User);
            return View(kitchens.ToList());
        }

        // GET: Kitchens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // GET: Kitchens/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, Convert.ToInt32(Session["Id"]), (string)Session["Name"]);
            return View();
        }

        // POST: Kitchens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitchenId,KitchenName,UserId,WorkingDays,selectedDays,StartTime,timeStart,CloseTime,timeClose,Image")] Kitchen kitchen)
        {

            if (ModelState.IsValid)
            {
                if (kitchen.timeStart.Length < 8 || kitchen.timeClose.Length < 8)
                {
                    kitchen.StartTime = DateTime.ParseExact(kitchen.timeStart, "h:mm tt", null);
                    kitchen.CloseTime = DateTime.ParseExact(kitchen.timeClose, "h:mm tt", null);

                }
                else
                {
                    kitchen.StartTime = DateTime.ParseExact(kitchen.timeStart, "hh:mm tt", null);
                    kitchen.CloseTime = DateTime.ParseExact(kitchen.timeClose, "hh:mm tt", null);
                }

                kitchen.WorkingDays = string.Join(", ", kitchen.selectedDays);
                db.Kitchens.Add(kitchen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, (string)Session["Id"], (string)Session["Name"], kitchen.UserId);
            return View(kitchen);
        }

        // GET: Kitchens/Edit/5
        [AuthorizeFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, (string)Session["Id"], (string)Session["Name"], kitchen.UserId);
            return View(kitchen);
        }

        // POST: Kitchens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitchenId,KitchenName,UserId,WorkingDays,StartTime,CloseTime,Image")] Kitchen kitchen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, (string)Session["Id"], (string)Session["Name"], kitchen.UserId);
            return View(kitchen);
        }

        // GET: Kitchens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // POST: Kitchens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitchen kitchen = db.Kitchens.Find(id);
            db.Kitchens.Remove(kitchen);
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
