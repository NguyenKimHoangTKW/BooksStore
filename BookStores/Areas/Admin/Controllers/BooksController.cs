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

namespace BookStores.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Books
        public ActionResult Index(int? size, int? page, string sortProperty, string searchString, string sortOrder = "", int bookid = 0)
        {
            ViewBag.Keyword = searchString;
            ViewBag.Subject = bookid;
            var book = db.Books.Include(p => p.Publisher).Include(p => p.OrderDetails).Include(p => p.WriteBooks).Include(p => p.BookCategory);

            if (!String.IsNullOrEmpty(searchString))
                book = book.Where(b => b.nameBooks.Contains(searchString));

            if (bookid != 0)
                book = book.Where(c => c.idBookCat == bookid);

            ViewBag.bookid = new SelectList(db.BookCategories, "idBookCat", "nameBookCat");

            if (sortOrder == "asc") ViewBag.SortOrder = "desc";
            if (sortOrder == "desc") ViewBag.SortOrder = "";
            if (sortOrder == "") ViewBag.SortOrder = "asc";
            // 2.1. Tạo thuộc tính sắp xếp mặc định là "Title"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "nameBooks";

            // 2.2. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
            if (sortOrder == "desc")
                book = book.OrderBy(sortProperty + " desc");
            else
                book = book.OrderBy(sortProperty);

            // 3 Đoạn code sau dùng để phân trang
            ViewBag.Page = page;

            // 3.1. Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 3.2. Thiết lập số trang đang chọn vào danh sách List<SelectListItem> items
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.Size = items;
            ViewBag.CurrentSize = size;
            // 3.3. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;

            // 3.4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            // 3.5. Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 3.6 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(book.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(book.ToPagedList(pageNumber, pageSize));
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
                if (Thumb != null && Thumb.ContentLength > 0)
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
            ViewBag.idBookCat = new SelectList(db.Topics, "idBookCat", "nameBookCat", book.idBookCat);
            ViewBag.idPublisher = new SelectList(db.Publishers, "idPublisher", "namePublisher", book.idPublisher);
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBooks,codeBooks,nameBooks,describe,Thumb,updateDay,quantity,price,idTopic,idPublisher")] Book book, HttpPostedFileBase Thumb, FormCollection form)
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
            db.Books.Remove(book);
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
