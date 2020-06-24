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
    public class MenusController : Controller
    {
        private feastfreedomEntities db = new feastfreedomEntities();

        // GET: Menus
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.Kitchen).Include(m => m.Kitchen1);
            return View(menus.ToList());
        }

        public ActionResult MenusItems(int? kitchenId)
        {
            var menu = db.Menus.Where(model => model.KitchenId == kitchenId).ToList(); ;

            if (kitchenId == null)
                menu = db.Menus.ToList();

            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        public ActionResult cart()
        {
            List<Menu> items = (List<Menu>)Session["cart"];
            
            return View(items);
        }
        
        
        public ActionResult Add(Menu menuItem)
        {
            if (Session["cart"] == null)
            {
                List<Menu> li = new List<Menu>();

                li.Add(menuItem);
                Session["cart"] = li;
                ViewBag.cart = li.Count();


                Session["count"] = 1;
                return RedirectToAction("Login", "Users");
            }
            else
            {
                List<Menu> li = (List<Menu>)Session["cart"];
                li.Add(menuItem);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Kitchens");
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName");
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,KitchenId,ItemName,VeganFriendly,MenuType,Ingredients,Image,Price")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            menu.Kitchen = db.Kitchens.Where(x=>x.KitchenId == menu.KitchenId).First();
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName", menu.KitchenId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName", menu.KitchenId);
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName", menu.KitchenId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,KitchenId,ItemName,VeganFriendly,MenuType,Ingredients,Image,Price")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName", menu.KitchenId);
            ViewBag.KitchenId = new SelectList(db.Kitchens, "KitchenId", "KitchenName", menu.KitchenId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
