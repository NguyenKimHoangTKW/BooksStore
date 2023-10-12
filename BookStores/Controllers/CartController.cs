using BookStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
namespace BookStores.Controllers
{
    public class CartController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        // GET: Cart
        public List<Cart> GetCarts()
        {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }

        public ActionResult AddCart(int ms, string url)
        {
            List<Cart> lstCart = GetCarts();
            Cart sp = lstCart.Find(n => n.iMaSach == ms);
            if (sp == null)
            {
                sp = new Cart(ms);
                lstCart.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

        //Tính tổng số lượng
        private int SumQuantity()
        {
            int iSumQuantity = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                iSumQuantity = lstCart.Sum(n => n.iSoLuong);
            }
            return iSumQuantity;
        }

        // Tính tổng tiền
        private double SumPrice()
        {
            double dSumPrice = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                dSumPrice = lstCart.Sum(n => n.ThanhTien);
            }
            return dSumPrice;
        }

        public ActionResult Cart()
        {
            List<Cart> lstCart = GetCarts();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.TongSoLuong = SumQuantity();
            ViewBag.TongTien = SumPrice();
            return View(lstCart);
        }

        public ActionResult CartPartial()
        {

            ViewBag.TongSoLuong = SumQuantity();
            ViewBag.TongTien = SumPrice();
            return PartialView();
        }
        public ActionResult DeleteBookByID(int iMaSach)
        {
            List<Cart> lstCart = GetCarts();
            Cart sp = lstCart.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (sp != null)
            {
                lstCart.RemoveAll(n => n.iMaSach == iMaSach);
                if (lstCart.Count == 0)
                {
                    return RedirectToAction("Index", "BookStore");

                }

            }
            return RedirectToAction("Cart");
        }

        public ActionResult UpdateCart(int iMaSach, FormCollection f)
        {
            List<Cart> lstCart = GetCarts();
            Cart sp = lstCart.SingleOrDefault(n => n.iMaSach == iMaSach);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteAllCart ()
        {
            List<Cart> lstCart = GetCarts();
            lstCart.Clear();
            return RedirectToAction("Index", "BookStore");
        }
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["Customer"] == null || Session["Customer"].ToString() == "")
            {
                return RedirectToAction("Login", "Users");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "BookStore");
            }
            List<Cart> lstCart = GetCarts();
            ViewBag.TongSoLuong = SumQuantity();
            ViewBag.TongTien = SumPrice();
            return View(lstCart);
        }

        [HttpPost]
        public ActionResult Order(FormCollection f)
        {
            Order order = new Order();
            Customer customer = (Customer)Session["Customer"];
            List<Cart> lstCart = GetCarts();
            order.codeOrder = "Order" + DateTime.Now.ToString("yyyyMMddHHmmss");
            order.idCustomer = customer.idCustomer;
            order.orderDate = DateTime.Now;
            TimeSpan threeDays = new TimeSpan(3, 0, 0, 0);
            order.deliveryDate = order.orderDate + threeDays;
            order.checkPay = false;
            order.deliveryStatus = 1;
            db.Orders.Add(order);
            db.SaveChanges();
            foreach (var item in lstCart)
            {
                OrderDetail orderdetail = new OrderDetail();
                orderdetail.idOrder = order.idOrder;
                orderdetail.idBooks = item.iMaSach;
                orderdetail.quantity = item.iSoLuong;
                orderdetail.price = (decimal)item.dDonGia;
                db.OrderDetails.Add(orderdetail);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("OrderConfirm", "Cart");

        }

        public ActionResult OrderConfirm()
        {
            return View();
        }
    }
}