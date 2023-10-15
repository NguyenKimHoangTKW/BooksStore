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
    public class TopicController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            
            var topic = db.Topics.Include(p => p.BookCategories);

            return View(topic.ToList());
        }

        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                if(db.Topics.SingleOrDefault(tp => tp.nameTopic == topic.nameTopic) != null)
                {
                    ViewBag.ThongBao = "Chủ đề này đã tồn tại, vui lòng nhập chủ đề khác";
                }
                else
                {
                    int nextId = GetNextId();
                    topic.idTopic = nextId;
                    topic.codeTopic = "CĐ" + nextId.ToString("2023BS"); ;
                    db.Topics.Add(topic);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(topic);
        }
        private int GetNextId()
        {
            int? maxId = db.Topics.Max(t => (int?)t.idTopic);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTopic,codeTopic,nameTopic")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Topic topics ,int id)
        {
            Topic topic = db.Topics.Find(id);
            if(db.BookCategories.Any(tp => tp.idTopic == topic.idTopic))
            {
                ViewBag.ThongBao = "Không thể xóa vì bị trùng khóa ngoại bên Thể loại sách";
            }
            else
            {
                db.Topics.Remove(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
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
