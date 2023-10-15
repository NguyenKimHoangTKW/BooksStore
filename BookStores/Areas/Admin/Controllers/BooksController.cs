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
using System.IO;
using BookStores.App_Start;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class BooksController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Books
        public ActionResult Index(int bookid = 0)
        {
            
            var book = db.Books.Include(p => p.Publisher).Include(p => p.OrderDetails).Include(p => p.WriteBooks).Include(p => p.BookCategory);

            int nxbid = 0;

            if (bookid != 0)
                book = book.Where(c => c.idBookCat == bookid);

            if (nxbid != 0)
                book = book.Where(c => c.idPublisher == bookid);
            ViewBag.bookid = new SelectList(db.BookCategories, "idBookCat", "nameBookCat");
            ViewBag.nxbid = new SelectList(db.Publishers, "idPublisher", "namePublisher");
            return View(book.ToList());
        }

        // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            ViewBag.idBookCat = new SelectList(db.BookCategories, "idBookCat", "nameBookCat");
            ViewBag.idPublisher = new SelectList(db.Publishers, "idPublisher", "namePublisher");
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Book book, HttpPostedFileBase Thumb, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                book.idBooks = nextId;
                book.codeBooks = "SACH" + nextId.ToString("2023BS");
                book.updateDay = DateTime.Now;
                if (db.Books.SingleOrDefault(b => b.nameBooks == book.nameBooks) != null)
                {
                    ViewBag.ThongBao = "Sách này đã tồn tại, vui long nhập sách mới";
                }
                else if (Thumb != null && Thumb.ContentLength > 0)
                {
                    string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                    string _Tail = Path.GetExtension(Thumb.FileName);
                    string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                    string _path = Path.Combine(Server.MapPath("~/Areas/Style/Images/ImagesBooks"), fullLink);
                    Thumb.SaveAs(_path);
                    book.Thumb = fullLink;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng chọn một tệp ảnh.");
                }
            }
            ViewBag.idBookCat = new SelectList(db.BookCategories, "idBookCat", "nameBookCat", book.idBookCat);
            ViewBag.idPublisher = new SelectList(db.Publishers, "idPublisher", "namePublisher", book.idPublisher);
            return View(book);
        }
        private int GetNextId()
        {
            int? maxId = db.Books.Max(t => (int?)t.idBooks);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBookCat = new SelectList(db.BookCategories, "idBookCat", "nameBookCat", book.idBookCat);
            ViewBag.idPublisher = new SelectList(db.Publishers, "idPublisher", "namePublisher", book.idPublisher);
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBooks,codeBooks,nameBooks,describe,Thumb,updateDay,quantity,price,idBookCat,idPublisher")] Book book, HttpPostedFileBase Thumb, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Thumb != null)
                    {
                        string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                        string _Tail = Path.GetExtension(Thumb.FileName);
                        string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                        string _path = Path.Combine(Server.MapPath("~/Areas/Style/Images/ImagesBooks"), fullLink);
                        Thumb.SaveAs(_path);
                        book.Thumb = fullLink;
                        _path = Path.Combine(Server.MapPath("~/Areas/Style/Images/ImagesBooks"), form["oldimage"]);

                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                    book.Thumb = form["oldimage"];
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.idBookCat = new SelectList(db.BookCategories, "idBookCat", "nameBookCat", book.idBookCat);
            ViewBag.idPublisher = new SelectList(db.Publishers, "idPublisher", "namePublisher", book.idPublisher);
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            if(db.WriteBooks.Any(a => a.idBooks == book.idBooks))
            {
                ViewBag.ThongBao = "Không thể xóa sách vì liên quan đến khóa ngoại của 'Người viết sách', vui lòng kiểm tra lại";
            }
            else if(db.OrderDetails.Any(a => a.idBooks == book.idBooks))
            {
                ViewBag.ThongBao = "Không thể xóa sách vì liên quan đến khóa ngoại của 'Chi Tiết hóa đơn', bạn hãy cập nhật số lượng sách xuống 0 thay vì xóa";
            }
            else
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
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
