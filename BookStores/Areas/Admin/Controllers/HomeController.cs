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
            var ordernon = "Đã tiếp nhận đơn hàng";
            var odeertransit = "Đang giao hàng";
            var ordersuccess = "Giao hàng thành công";
            IList<Book> listsp = db.Books.ToList();
            IList<Customer> listcustomer = db.Customers.ToList();
            IList<Author> listauthor = db.Authors.ToList();
            IList<Order> lstordernon = db.Orders.Where(p => p.deliveryStatus.Equals(ordernon)).ToList();
            IList<Order> lstordertransit = db.Orders.Where(p => p.deliveryStatus.Equals(odeertransit)).ToList();
            IList<Order> lstordersuccess = db.Orders.Where(p => p.deliveryStatus.Equals(ordersuccess)).ToList();
            IList<Order> lstordernonpay = db.Orders.Where(p => p.checkPay == false).ToList();
            decimal SumPrice = db.OrderDetails.Sum(o => o.price);
            ViewBag.TotalBook = listsp.Count;
            ViewBag.TotalCustomer = listcustomer.Count;
            ViewBag.TotalAuthor = listauthor.Count;
            ViewBag.TotalOrderNon = lstordernon.Count;
            ViewBag.TotalOrderTranSit = lstordertransit.Count;
            ViewBag.TotalOrderSuccess = lstordersuccess.Count;
            ViewBag.TotalOrderNonPay = lstordernonpay.Count;
            ViewBag.SumPrice = SumPrice;
            return View();
        }
    }
}