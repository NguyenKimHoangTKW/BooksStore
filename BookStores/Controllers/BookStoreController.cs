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
            var listbooksnew = GetBooksNew(20);
            return View(listbooksnew);
        }
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
            var book = from s in db.Books 
                       from tp in db.Topics
                       from bc in db.BookCategories
                       where s.idBookCat == bc.idBookCat
                       where bc.idTopic == tp.idTopic
                       where tp.idTopic == id select s;
            return View(book);
        }
        public ActionResult BookCatPartial(int id)
        {
            var bookcat = from s in db.BookCategories where s.idTopic == id select s;
            return PartialView(bookcat);
        }
        public ActionResult BooksByCategory(int id)
        {
            var book = from s in db.Books where s.idBookCat == id select s;
            return View(book);
        }
    }
}