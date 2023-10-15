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
    public class CustomerController : Controller
    {
        
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Customer
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: Admin/Customer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCustomer,codeCustomer,nameCustomer,userName,passWord,email,address,phone,birthDay,creDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                customer.idCustomer = nextId;
                customer.codeCustomer = "KH" + nextId.ToString("2023BS");
                customer.creDate = DateTime.Now;
                if(db.Customers.SingleOrDefault(c => c.userName == customer.userName) != null)
                {
                    ViewBag.ThongBao = "Tài khoản đã tồn tại, vui lòng chọn tài khoản khác";
                }
                else if(db.Customers.SingleOrDefault(c => c.email == customer.email) != null)
                {
                    ViewBag.ThongBao = "Email đã tồn tại, vui lòng chọn Email khác";
                }
                else
                {
                    db.Customers.Add(customer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }
        private int GetNextId()
        {
            int? maxId = db.Customers.Max(t => (int?)t.idCustomer);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/Customer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCustomer,codeCustomer,nameCustomer,userName,passWord,email,address,phone,birthDay,creDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/Customer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if(db.Orders.Any(c => c.idCustomer == customer.idCustomer))
            {
                ViewBag.ThongBao = "Không thể xóa khách hàng vì khách hàng có tồn tại hóa đơn, vui lòng 'Clear' hết hóa đơn khách khi xóa";
            }
            else
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
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
