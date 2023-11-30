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
    }
}