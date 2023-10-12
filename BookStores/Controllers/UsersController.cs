using BookStores.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookStores.Controllers
{
    public class UsersController : Controller
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        // GET: Users
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin, Customer customer)
        {
            var isLoginAdmin = db.Admins.SingleOrDefault(sa => sa.userName.Equals(admin.userName) && sa.passWord.Equals(admin.passWord));
            var isLoginCustomer = db.Customers.SingleOrDefault(cus => cus.userName.Equals(customer.userName) && cus.passWord.Equals(customer.passWord));
            if(isLoginAdmin != null)
            {
                Session["Admin"] = isLoginAdmin;
                return RedirectToAction("Index","Home", new {area = "Admin"});
            }
            else if(isLoginCustomer != null)
            {
                Session["Customer"] = isLoginCustomer;
                return RedirectToAction("Index", "BookStore", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cus)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                cus.idCustomer = nextId;
                cus.codeCustomer = "KH" + nextId.ToString("2023BS");
                cus.creDate = DateTime.Now;
                if (db.Customers.SingleOrDefault(c => c.userName == cus.userName) != null)
                {
                    ViewBag.ThongBao = "Tài khoản đã tồn tại, vui lòng nhập tài khoản khác";
                }
                else if (db.Customers.SingleOrDefault(c => c.email == cus.email) != null)
                {
                    ViewBag.ThongBao = "Email này đã tồn tại, vui lòng nhập Email khác";
                }
                else
                {
                    db.Customers.Add(cus);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
            }
            
            return View();
        }
        private int GetNextId()
        {
            // Tìm giá trị id tiếp theo trong bảng
            int? maxId = db.Customers.Max(t => (int?)t.idCustomer);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "BookStore", new {area =""});
        }
        [HttpGet]
        public ActionResult TitleUser()
        {
            var usercustomer = Session["Customer"] as BookStores.Models.Customer;
            if (usercustomer != null)
            {
                return View(usercustomer);
            }
            return View(usercustomer);  
        }

        [HttpPost]
        public ActionResult TitleUser(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var usercustomer = Session["Customer"] as BookStores.Models.Customer;
                if (usercustomer != null)
                {
                    // Update the customer data from the session
                    usercustomer.nameCustomer = customer.nameCustomer;
                    usercustomer.email = customer.email;
                    usercustomer.userName = customer.userName;
                    usercustomer.passWord = customer.passWord;
                    usercustomer.phone = customer.phone;
                    usercustomer.birthDay = customer.birthDay;
                    usercustomer.address = customer.address;

                    // Save the changes to the database
                    try
                    {
                        db.Entry(usercustomer).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("TitleUser");
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        ModelState.AddModelError("", "An error occurred while saving data to the database.");
                    }
                }
            }

            return View(customer);
        }
    }
}