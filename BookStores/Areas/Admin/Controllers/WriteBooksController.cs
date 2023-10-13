using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStores.Models;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using PagedList;
using BookStores.App_Start;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class WriteBooksController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/WriteBooks
        public ActionResult Index()
        {
            
            var writebooks = db.WriteBooks.Include(p => p.Author).Include(p => p.Book);

           return View(writebooks.ToList());
        }

        // GET: Admin/WriteBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriteBook writeBook = db.WriteBooks.Find(id);
            if (writeBook == null)
            {
                return HttpNotFound();
            }
            return View(writeBook);
        }

        // GET: Admin/WriteBooks/Create
        public ActionResult Create()
        {
            ViewBag.idAuthor = new SelectList(db.Authors, "idAuthor", "nameAuthor");
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "nameBooks");
            return View();
        }

        // POST: Admin/WriteBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WriteBook writeBook)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                writeBook.idWriteBook = nextId;
                db.WriteBooks.Add(writeBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAuthor = new SelectList(db.Authors, "idAuthor", "nameAuthor", writeBook.idAuthor);
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "nameBooks", writeBook.idBooks);
            return View(writeBook);
        }
        private int GetNextId()
        {
            // Tìm giá trị id tiếp theo trong bảng
            int? maxId = db.WriteBooks.Max(t => (int?)t.idWriteBook);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }

        // GET: Admin/WriteBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriteBook writeBook = db.WriteBooks.Find(id);
            if (writeBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAuthor = new SelectList(db.Authors, "idAuthor", "nameAuthor", writeBook.idAuthor);
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "nameBooks", writeBook.idBooks);
            return View(writeBook);
        }

        // POST: Admin/WriteBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAuthor,idBooks,access,location,idWriteBook")] WriteBook writeBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(writeBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAuthor = new SelectList(db.Authors, "idAuthor", "nameAuthor", writeBook.idAuthor);
            ViewBag.idBooks = new SelectList(db.Books, "idBooks", "nameBooks", writeBook.idBooks);
            return View(writeBook);
        }

        // GET: Admin/WriteBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WriteBook writeBook = db.WriteBooks.Find(id);
            if (writeBook == null)
            {
                return HttpNotFound();
            }
            return View(writeBook);
        }

        // POST: Admin/WriteBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WriteBook writeBook = db.WriteBooks.Find(id);
            db.WriteBooks.Remove(writeBook);
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
