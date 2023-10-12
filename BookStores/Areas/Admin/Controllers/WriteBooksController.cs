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
        public ActionResult Index(int? size, int? page, string sortProperty, string searchString, string sortOrder = "", int writebooksid = 0)
        {
            ViewBag.Keyword = searchString;
            ViewBag.Subject = writebooksid;
            var writebooks = db.WriteBooks.Include(p => p.Author).Include(p => p.Book);

            if (!String.IsNullOrEmpty(searchString))
                writebooks = writebooks.Where(b => b.Book.nameBooks.Contains(searchString));


            if (sortOrder == "asc") ViewBag.SortOrder = "desc";
            if (sortOrder == "desc") ViewBag.SortOrder = "";
            if (sortOrder == "") ViewBag.SortOrder = "asc";
            // 2.1. Tạo thuộc tính sắp xếp mặc định là "Title"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "idBooks";

            // 2.2. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
            if (sortOrder == "desc")
                writebooks = writebooks.OrderBy(sortProperty + " desc");
            else
                writebooks = writebooks.OrderBy(sortProperty);

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
            int checkTotal = (int)(writebooks.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(writebooks.ToPagedList(pageNumber, pageSize));
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
