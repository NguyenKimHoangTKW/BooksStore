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
using System.Linq.Dynamic;
using System.Linq.Expressions;
using PagedList;
using System.EnterpriseServices;
using BookStores.App_Start;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class BookCategoriesController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/BookCategories
        public async Task<ActionResult> Index(int topicid = 0)
        {
            var bookcat = db.BookCategories.Include(o => o.Topic).Include(o => o.Books);
            if (topicid != 0)
                bookcat = bookcat.Where(c => c.idTopic == topicid);

            ViewBag.topicid = new SelectList(db.Topics, "idTopic", "nameTopic");
            return View(await bookcat.ToListAsync());
        }

        // GET: Admin/BookCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // GET: Admin/BookCategories/Create
        public ActionResult Create()
        {
            ViewBag.idTopic = new SelectList(db.Topics, "idTopic", "nameTopic");
            return View();
        }

        // POST: Admin/BookCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                bookCategory.idBookCat = nextId;
                bookCategory.codeBookCat = "TL" + nextId.ToString("2023BS");
                if (db.BookCategories.SingleOrDefault(bc => bc.nameBookCat == bookCategory.nameBookCat) != null)
                {
                    ViewBag.ThongBao = "Thể loại sách đã tồn tại, vui lòng chọn thể loại sách khác";
                }
                else
                {
                    db.BookCategories.Add(bookCategory);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                } 
            }

            ViewBag.idTopic = new SelectList(db.Topics, "idTopic", "nameTopic", bookCategory.idTopic);
            return View(bookCategory);
        }
        private int GetNextId()
        {
            int? maxId = db.BookCategories.Max(t => (int?)t.idBookCat);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }

        // GET: Admin/BookCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTopic = new SelectList(db.Topics, "idTopic", "nameTopic", bookCategory.idTopic);
            return View(bookCategory);
        }

        // POST: Admin/BookCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idBookCat,codeBookCat,idTopic,nameBookCat")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idTopic = new SelectList(db.Topics, "idTopic", "nameTopic", bookCategory.idTopic);
            return View(bookCategory);
        }

        // GET: Admin/BookCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: Admin/BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookCategory bookCategory = await db.BookCategories.FindAsync(id);
            if (db.Books.Any(b => b.idBookCat == bookCategory.idBookCat))
            {
                ViewBag.ThongBao = "Không thể xóa vì bị trùng khóa ngoại bên Thể loại sách";
            }
            else
            {
                db.BookCategories.Remove(bookCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookCategory);
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
