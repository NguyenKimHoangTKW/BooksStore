using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStores.Models;
using BookStores.App_Start;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class OrderDetailsController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/OrderDetails
        public async Task<ActionResult> Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Book).Include(o => o.Order);
            return View(await orderDetails.ToListAsync());
        }

        // GET: Admin/OrderDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "codeBooks");
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "codeOrder");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idOrder,idBooks,quantity,price")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "codeBooks", orderDetail.idBooks);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "codeOrder", orderDetail.idOrder);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "codeBooks", orderDetail.idBooks);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "codeOrder", orderDetail.idOrder);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOrder,idBooks,quantity,price")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "codeBooks", orderDetail.idBooks);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "codeOrder", orderDetail.idOrder);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            db.OrderDetails.Remove(orderDetail);
            await db.SaveChangesAsync();
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
