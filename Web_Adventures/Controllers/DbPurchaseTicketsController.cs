using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Adventures.Models;

namespace Web_Adventures.Controllers
{
    public class DbPurchaseTicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DbPurchaseTicket
        public ActionResult Index()
        {
            return View(db.orderRequest.ToList());
        }

        // GET: DbPurchaseTicket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbPurchaseTickets dbPurchaseTicket = db.orderRequest.Find(id);
            if (dbPurchaseTicket == null)
            {
                return HttpNotFound();
            }
            return View(dbPurchaseTicket);
        }

        // GET: DbPurchaseTicket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DbPurchaseTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FilledTime,BeginPoint,EndPoint,Price,RestaurantFood,Fridge,AdditionalServicePrice")] DbPurchaseTickets dbPurchaseTicket)
        {
            if (ModelState.IsValid)
            {
                db.orderRequest.Add(dbPurchaseTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbPurchaseTicket);
        }

        // GET: DbPurchaseTicket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbPurchaseTickets dbPurchaseTicket = db.orderRequest.Find(id);
            if (dbPurchaseTicket == null)
            {
                return HttpNotFound();
            }
            return View(dbPurchaseTicket);
        }

        // POST: DbPurchaseTicket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FilledTime,BeginPoint,EndPoint,Price,RestaurantFood,Fridge,AdditionalServicePrice")] DbPurchaseTickets dbPurchaseTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dbPurchaseTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dbPurchaseTicket);
        }

        // GET: DbPurchaseTicket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbPurchaseTickets dbPurchaseTicket = db.orderRequest.Find(id);
            if (dbPurchaseTicket == null)
            {
                return HttpNotFound();
            }
            return View(dbPurchaseTicket);
        }

        // POST: DbPurchaseTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DbPurchaseTickets dbPurchaseTicket = db.orderRequest.Find(id);
            db.orderRequest.Remove(dbPurchaseTicket);
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
