using BookStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStores.Controllers
{
    public class BookStoreController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        // GET: BookStore
        public ActionResult Index()
        {
            var listbooksnew = GetBooksNew(2);
            return View(listbooksnew);
        }
        [HttpGet]
        public ActionResult TopicPartialView()
        {
            var listTopic = from tp in db.Topics select tp;
            return PartialView(listTopic);
        }
        private List<Book> GetBooksNew(int count)
        {
            return db.Books.OrderByDescending(a => a.updateDay).Take(count).ToList();
        }
        public ActionResult BookDetail(int id)
        {
            var book = from s in db.Books
                       where s.idBooks == id
                       select s;
            return PartialView(book.Single());
        }
        public ActionResult BooksByTopic(int id)
        {
            var book = from s in db.Books where s.idTopic == id select s;
            return View(book);
        }
    }
}