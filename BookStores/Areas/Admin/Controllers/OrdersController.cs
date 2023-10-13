﻿using System;
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
    public class OrdersController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Orders
        public async Task<ActionResult> Index()
        {
            
            var orders = db.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }
        public ActionResult OrderNon()
        {
            var non = "Đã tiếp nhận đơn hàng";
            var ordernon = from o in db.Orders
                           where o.deliveryStatus.Equals(non)
                           select o;
            return View(ordernon);
        }
        public ActionResult OrderInTransit()
        {
            var intransit = "Đang giao hàng";
            var orderintransit = from o in db.Orders
                           where o.deliveryStatus.Equals(intransit)
                           select o;
            if (db.Orders.SingleOrDefault(x => x.deliveryStatus.Equals(intransit)) == null)
            {
                return RedirectToAction("Error", "Orders");
            }
            else
            {
                return View(orderintransit);
            }
            
        }
        public ActionResult OrderSuccessfully()
        {
            var successfully = "Giao hàng thành công";

            var ordersuccessfully = from o in db.Orders
                                    where o.deliveryStatus.Equals(successfully)
                                    select o;
            if (db.Orders.SingleOrDefault(x => x.deliveryStatus.Equals(successfully)) == null)
            {
                return RedirectToAction("Error", "Orders");
            }
            else
            {
                return View(ordersuccessfully);
            }
           
        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: Admin/Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        // GET: Admin/Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "nameCustomer", order.idCustomer);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOrder,codeOrder,checkPay,deliveryStatus,orderDate,deliveryDate,idCustomer")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "nameCustomer", order.idCustomer);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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
