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
    public class AuthorController : Controller
    {

        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Author
        public ActionResult Index()
        {
            
            var author = db.Authors.Include(p => p.WriteBooks);

            return View(author.ToList());
        }

        // GET: Admin/Author/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Admin/Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                author.idAuthor = nextId;
                author.codeAuthor = "TG" + nextId.ToString("2023BS"); ;
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }
        private int GetNextId()
        {
            int? maxId = db.Authors.Max(t => (int?)t.idAuthor);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/Author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin/Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAuthor,codeAuthor,nameAuthor,address,story,phone")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Admin/Author/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Admin/Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            if(db.WriteBooks.Any(x => x.idAuthor == author.idAuthor))
            {
                ViewBag.ThongBao = "Không thể xóa tác giả vì bị trùng khóa ngoại bên 'Người viết sách', vui lòng kiểm tra lại";
            }
            else
            {
                db.Authors.Remove(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
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
