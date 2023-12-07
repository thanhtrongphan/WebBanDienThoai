using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    public class HomeController : Controller
    {
        private WEBDIENTHOAIEntities2 db = new WEBDIENTHOAIEntities2();
        public ActionResult Index()
        {
            var products = db.PRODUCTs.ToList();
            return View(products);
        }
        public ActionResult showProductbyBrand(int idBrand)
        {
            var products = db.PRODUCTs.Where(p => p.BrandID == idBrand).ToList();
            return View("Index",products);
        }
        [ChildActionOnly]
        public ActionResult MenuPartial()
        {
            var brands = db.BRANDs.ToList();
            return PartialView("MenuPartial",brands);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}