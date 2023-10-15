using BookStores.Models;
using System;
using System.CodeDom;
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
        public ActionResult Login(UserAdmin admin, Customer customer, FormCollection f)
        {
            var username = f["username"];
            var password = f["password"];
            var isLoginAdmin = db.UserAdmins.SingleOrDefault(sa => sa.userName.Equals(admin.userName) && sa.passWord.Equals(admin.passWord));
            var isLoginCustomer = db.Customers.SingleOrDefault(cus => cus.userName.Equals(customer.userName) && cus.passWord.Equals(customer.passWord));
            if(isLoginAdmin != null)
            {
                admin.userName = username;
                admin.passWord = password;
                Session["Admin"] = isLoginAdmin;
                return RedirectToAction("Index","Home", new {area = "Admin"});
            }
            else if(isLoginCustomer != null)
            {
                customer.userName = username;
                customer.passWord = password;
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
        public ActionResult Register(Customer cus, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                cus.idCustomer = nextId;
                cus.codeCustomer = "KH" + nextId.ToString("2023BS");
                cus.creDate = DateTime.Now;
                var password = f["Password"];
                var checkpassword = f["CheckPassword"];
                if (db.Customers.SingleOrDefault(c => c.userName == cus.userName) != null)
                {
                    TempData["SweetAlertMessage"] = "Tài khoản đã tồn tại, vui lòng nhập tài khoản khác";
                    TempData["SweetAlertType"] = "error";
                }
                else if (db.Customers.SingleOrDefault(c => c.email == cus.email) != null)
                {
                    TempData["SweetAlertMessage"] = "Email này đã tồn tại, vui lòng nhập Email khác";
                    TempData["SweetAlertType"] = "error";

                }
                else if (password != checkpassword)
                {
                    TempData["SweetAlertMessage"] = "Mật khẩu nhập lại không đúng";
                    TempData["SweetAlertType"] = "error";
                }
                else
                {
                    cus.passWord = password;
                    TempData["SweetAlertMessage"] = "Đăng ký thành công";
                    TempData["SweetAlertType"] = "success";
                    db.Customers.Add(cus);
                    db.SaveChanges();
                   
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
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string username, string password, string confirmPassword)
        {
            // Tìm khách hàng theo email
            var customer = db.Customers.SingleOrDefault(c => c.email == username);

            if (customer != null)
            {
                if (password != confirmPassword)
                {
                    TempData["SweetAlertMessage"] = "Mật khẩu và mật khẩu xác nhận không khớp.";
                    TempData["SweetAlertType"] = "error";
                    return View();
                }

                customer.passWord = password;

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                TempData["SweetAlertMessage"] = "Mật khẩu đã được thay đổi thành công.";
                TempData["SweetAlertType"] = "success";

                return RedirectToAction("Login"); 
            }
            else
            {
                TempData["SweetAlertMessage"] = "Không tìm thấy khách hàng với email này.";
                TempData["SweetAlertType"] = "error";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "BookStore", new {area =""});
        }

        public ActionResult TitleUser(int? id)
        {
            var titlecus = from cus in db.Customers
                           where cus.idCustomer == id select cus;
            return View(titlecus);
        }
        public ActionResult EditTitleUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTitleUser([Bind(Include = "idCustomer,codeCustomer,nameCustomer,userName,passWord,email,address,phone,birthDay")] Customer customer, FormCollection collection)
        {
            var usercustomer = Session["Customer"] as BookStores.Models.Customer;
            if (ModelState.IsValid)
            {
                var password = collection["password"]; 
                var CheckPassword = collection["checkpassword"];

                if (password != CheckPassword)
                {
                    TempData["SweetAlertMessage"] = "Mật khẩu nhập lại không chính xác, vui lòng kiểm tra lại";
                    TempData["SweetAlertType"] = "error";
                }
                else
                {
                        TempData["SweetAlertMessage"] = "Thay đổi thông tin thành công";
                        TempData["SweetAlertType"] = "success";
                        customer.passWord = password;
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                        
                }
                
            }
            return View(customer);
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}