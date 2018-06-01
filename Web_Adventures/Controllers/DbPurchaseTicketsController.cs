using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Web_Adventures.Models;
using OfficeOpenXml;
using System.IO;
using System.Web.Hosting;

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

        public ActionResult Save(int? id)
        {
            var dbcontext = new ApplicationDbContext();
            var order = dbcontext.orderRequest.Find(id);

            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead(HostingEnvironment.ApplicationPhysicalPath + "information.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }

            var worksheet = pkg.Workbook.Worksheets[1];
            worksheet.Cells[4, 2].Value = "Begin point:";
            worksheet.Cells[4, 3].Value = order.BeginPoint;
            worksheet.Cells[5, 2].Value = "End point:";
            worksheet.Cells[5, 3].Value = order.EndPoint;
            worksheet.Cells[6, 2].Value = "Price (rubles):";
            worksheet.Cells[6, 3].Value = order.Price;
            worksheet.Cells[7, 2].Value = "Restaurant food:";
            worksheet.Cells[7, 3].Value = order.RestaurantFood;
            worksheet.Cells[8, 2].Value = "Fridge:";
            worksheet.Cells[8, 3].Value = order.Fridge;
            worksheet.Cells[9, 2].Value = "Additional service price (rubles):";
            worksheet.Cells[9, 3].Value = order.AdditionalServicePrice;
            worksheet.Cells[5, 1].Value = "Filled time:";
            worksheet.Cells[6, 1].Value = order.FilledTime.ToString();

            worksheet.Cells.AutoFitColumns();
            var ms = new MemoryStream();
            pkg.SaveAs(ms);
            return File(ms.ToArray(), "application/ooxml", (order.FilledTime.ToString()).Replace(" ", "") + ".xlsx");
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
