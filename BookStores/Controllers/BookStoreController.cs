using BookStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace BookStores.Controllers
{
    public class BookStoreController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        // GET: BookStore
        public ActionResult Index()
        {
            var listbooksnew = db.Books.OrderByDescending(a => a.updateDay).Take(15).ToList();
            return View(listbooksnew);
        }
        public ActionResult GetAllBooksByDefaul(int? page)
        {
            int iSize = 30;
            int iPageNumber = (page ?? 1);
            var listbooksnew = db.Books.OrderByDescending(a => a.updateDay).ToList();
            return View(listbooksnew.ToPagedList(iPageNumber, iSize));
        }
        public ActionResult GetAllBooks(string searchString)
        {
            var listbooks = from b in db.Books
                            select b;
            ViewBag.Keyword = searchString;
            if (!String.IsNullOrEmpty(searchString))
                listbooks = listbooks.Where(b => b.nameBooks.Contains(searchString)).OrderByDescending(a => a.updateDay);
            return View(listbooks.ToList());
        }
        public ActionResult TopicPartialView()
        {
            var listTopic = from tp in db.Topics select tp;
            return PartialView(listTopic);
        }
      
        public ActionResult BookDetail(int id)
        {
            var book = from s in db.Books
                       where s.idBooks == id
                       select s;
            return PartialView(book.Single());
        }
        public ActionResult BooksByTopic(int id, int? page)
        {
            ViewBag.idTopic = id;
            int iSize = 15;
            int iPageNumber = (page ?? 1);
            var book = (from s in db.Books 
                       from tp in db.Topics
                       from bc in db.BookCategories
                       where s.idBookCat == bc.idBookCat
                       where bc.idTopic == tp.idTopic
                       where tp.idTopic == id select s).OrderBy(s =>s.idBooks);
            return View(book.ToPagedList(iPageNumber, iSize));
        }
        public ActionResult BookCatPartial(int id)
        {
            var bookcat = from s in db.BookCategories where s.idTopic == id select s;
            return PartialView(bookcat);
        }
        public ActionResult BooksByCategory(int id, int? page)
        {
            ViewBag.idCat = id;
            int iSize = 15;
            int iPageNumber = (page ?? 1);
            var book = (from s in db.Books where s.idBookCat == id select s).OrderBy(s => s.idBooks);
            return View(book.ToPagedList(iPageNumber, iSize));
        }
       
    }
}