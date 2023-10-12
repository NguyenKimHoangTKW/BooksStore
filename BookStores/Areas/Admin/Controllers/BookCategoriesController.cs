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

namespace BookStores.Areas.Admin.Controllers
{
    public class BookCategoriesController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/BookCategories
        public async Task<ActionResult> Index(int? size, int? page, string sortProperty, string searchString, string sortOrder = "", int bookcatid = 0)
        {
            ViewBag.Keyword = searchString;
            ViewBag.Subject = bookcatid;
            var bookcat = db.BookCategories.Include(p => p.Topic).Include(p => p.Books);

            if (!String.IsNullOrEmpty(searchString))
                bookcat = bookcat.Where(b => b.nameBookCat.Contains(searchString));

            if (bookcatid != 0)
                bookcat = bookcat.Where(c => c.idTopic == bookcatid);

            ViewBag.bookcatid = new SelectList(db.Topics, "idTopic", "nameTopic");

            ViewBag.bookid = new SelectList(db.BookCategories, "idBookCat", "nameBookCat");
            if (sortOrder == "asc") ViewBag.SortOrder = "desc";
            if (sortOrder == "desc") ViewBag.SortOrder = "";
            if (sortOrder == "") ViewBag.SortOrder = "asc";
            // 2.1. Tạo thuộc tính sắp xếp mặc định là "Title"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "nameBookCat";

            // 2.2. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
            if (sortOrder == "desc")
                bookcat = bookcat.OrderBy(sortProperty + " desc");
            else
                bookcat = bookcat.OrderBy(sortProperty);

            // 3 Đoạn code sau dùng để phân trang
            ViewBag.Page = page;

            // 3.1. Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
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
            int pageSize = (size ?? 20);

            ViewBag.pageSize = pageSize;

            // 3.5. Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 3.6 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(bookcat.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(bookcat.ToPagedList(pageNumber, pageSize));
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
            db.BookCategories.Remove(bookCategory);
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
