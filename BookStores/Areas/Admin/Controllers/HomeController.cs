using BookStores.App_Start;
using BookStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace BookStores.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            IList<Book> listsp = db.Books.ToList();
            IList<Customer> listcustomer = db.Customers.ToList();
            IList<Author> listauthor = db.Authors.ToList();
            ViewBag.TotalBook = listsp.Count;
            ViewBag.TotalCustomer = listcustomer.Count;
            ViewBag.TotalAuthor = listauthor.Count;
            return View();
        }
    }
}