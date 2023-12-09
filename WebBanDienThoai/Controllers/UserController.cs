using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    public class UserController : Controller
    {
        private WEBDIENTHOAIEntities2 db = new WEBDIENTHOAIEntities2();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string userMail = userlog["Username"].ToString();
            string password = userlog["Password"].ToString();
            var islogin = db.Accounts.SingleOrDefault(x => x.EMAIL.Equals(userMail) && x.PASSWORD.Equals(password));

            if (islogin != null)
            {
                if (userMail == "ADMIN@GMAIL.COM")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/AccountTypes");
                }
                else
                {
                    Session["use"] = islogin;
                    CartController cartController = new CartController();
                    //ViewBag.CountCart = (int)cartController.CountCart();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection dangkyForm)
        {
            string userMail = dangkyForm["EMAIL"].ToString();
            string password = dangkyForm["PASSWORD"].ToString();
            string name = dangkyForm["NAME"].ToString();
            string phone = dangkyForm["NUMBERPHONE"].ToString();
            string address = dangkyForm["ADDRESS"].ToString();
            string picture = dangkyForm["PICTURE"].ToString();
            var isregister = db.Accounts.SingleOrDefault(x => x.EMAIL.Equals(userMail));
            if (isregister != null)
            {
                ViewBag.isreg = "Email này đã dược đăng ký";
                return View("Register");
            }
            else
            {
                ViewBag.isreg = "";
                Account kh = new Account();
                kh.EMAIL = userMail;
                kh.NAME = name;
                kh.PASSWORD = password;
                kh.NUMBERPHONE = phone;
                kh.TYPEID = 2;
                kh.ADDRESS = address;
                kh.PICTURE = picture;
                db.Accounts.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
        }
        public ActionResult Profile(int id)
        {
            Session["idAcc"] = id;
            var account = db.Accounts.SingleOrDefault(x => x.ID == id);
            return View(account);
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(string password, string passwordNew, string passwordAgain)
        {
            int id = Convert.ToInt32(Session["idAcc"].ToString());
            var account = db.Accounts.SingleOrDefault(x => x.ID == id);
            if (password == account.PASSWORD)
            {
                if (passwordNew == passwordAgain)
                {
                    account.PASSWORD = passwordNew;
                    db.SaveChanges();
                    return RedirectToAction("Profile", "User", new { id = account.ID });
                }
                else
                {
                    ViewBag.Error = "Mật khẩu mới không trùng khớp";
                    return View("Edit", account);
                }
            }
            else
            {
                ViewBag.Error = "Mật khẩu cũ không chính xác";
                return View("Edit", account);
            }
        }
    }
}