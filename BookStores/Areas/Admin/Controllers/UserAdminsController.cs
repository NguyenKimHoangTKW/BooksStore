using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStores.App_Start;
using BookStores.Models;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class UserAdminsController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/UserAdmins
        public ActionResult Index()
        {
            return View(db.UserAdmins.ToList());
        }

        // GET: Admin/UserAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdmin userAdmin = db.UserAdmins.Find(id);
            if (userAdmin == null)
            {
                return HttpNotFound();
            }
            return View(userAdmin);
        }

        // GET: Admin/UserAdmins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAdmin,codeAdmin,nameAdmin,phone,userName,passWord,access")] UserAdmin userAdmin)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                userAdmin.idAdmin = nextId;
                userAdmin.codeAdmin = "ADMIN" + nextId.ToString("2023BS");
                userAdmin.access = "Đang hoạt động";
                if (db.UserAdmins.SingleOrDefault(u => u.userName == userAdmin.userName) != null)
                {
                    ViewBag.ThongBao = "Tài khoản này đã tổn tại, vui lòng chọn tài khoản khác";
                }
                else
                {
                    db.UserAdmins.Add(userAdmin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            return View(userAdmin);
        }
        private int GetNextId()
        {
            int? maxId = db.UserAdmins.Max(t => (int?)t.idAdmin);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/UserAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdmin userAdmin = db.UserAdmins.Find(id);
            if (userAdmin == null)
            {
                return HttpNotFound();
            }
            return View(userAdmin);
        }

        // POST: Admin/UserAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAdmin,codeAdmin,nameAdmin,phone,userName,passWord,access")] UserAdmin userAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAdmin);
        }

        // GET: Admin/UserAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdmin userAdmin = db.UserAdmins.Find(id);
            if (userAdmin == null)
            {
                return HttpNotFound();
            }
            return View(userAdmin);
        }

        // POST: Admin/UserAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAdmin userAdmin = db.UserAdmins.Find(id);
            db.UserAdmins.Remove(userAdmin);
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
