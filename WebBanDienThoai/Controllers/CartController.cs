using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    public class CartController : Controller
    {
        WebBanDienThoai.Models.WEBDIENTHOAIEntities2 db = new WebBanDienThoai.Models.WEBDIENTHOAIEntities2();
        public List<Cart_DTO> getCart()
        {
            List<Cart_DTO> lstGioHang = Session["cart"] as List<Cart_DTO>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<Cart_DTO>();
                Session["cart"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult Cart()
        {
            List<Cart_DTO> lstGioHang;
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang = getCart();
            return View(lstGioHang);
        }
        //tạo view con giỏ hàng
        public ActionResult CartPartial()
        {
            if (CountCart() == 0)
            {
                ViewBag.CountCart = 0;
                return PartialView();
            }
            ViewBag.CountCart = CountCart();
            return PartialView();
        }
        public ActionResult AddCart(int id)
        {
            if (Session["use"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lấy ra session giỏ hàng
            List<Cart_DTO> lstGioHang = getCart();
            //Kiểm tra sản phẩm này có tồn tại trong session["cart"] chưa?
            Cart_DTO sanpham = lstGioHang.Find(n => n.iMaSP == id);
            if (sanpham == null)
            {
                sanpham = new Cart_DTO(id);
                lstGioHang.Add(sanpham);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sanpham.iSoLuong++;
                return RedirectToAction("Index", "Home");
            }
        }

        //Tính tổng số lượng
        public int CountCart()
        {
            List<Cart_DTO> lstGioHang;
            int iTongSoLuong = 0;
            // if session cart is null
            if (Session["cart"] == null)
            {
                return 0;
            }
            
            lstGioHang = Session["cart"] as List<Cart_DTO>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        public Double SumCart()
        {
            List<Cart_DTO> lstGioHang;
            Double iTongTien = 0;
            // if session cart is null
            if (Session["cart"] == null)
            {
                return 0;
            }

            lstGioHang = Session["cart"] as List<Cart_DTO>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        // đặt hàng
        public ActionResult Order()
        {
            if (Session["use"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart_DTO> lstGioHang = getCart();
            Account acc = (Account)Session["use"];
            AccountCart_DTO accCart = new AccountCart_DTO(lstGioHang, acc);
            ViewBag.SumCart = SumCart();
            return View(accCart);
        }
        [HttpPost]
        // lưu đơn hàng
        public ActionResult SaveOrder(FormCollection donhangForm)
        {
            //kiểm tra đăng nhập
            if (Session["use"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //kiểm tra giỏ hàng
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string diachinhanhang = donhangForm["Account.ADDRESS"].ToString();
            //thêm đơn hàng
            BILL dh = new BILL();
            Account kh = (Account)Session["use"];
            List<Cart_DTO> lstGioHang = getCart();
            dh.AccountID = kh.ID;
            dh.DateBuy = DateTime.Now;
            dh.Status = 0;
            dh.Address = diachinhanhang;
            dh.TotalPrice = (int)SumCart();
            db.BILLs.Add(dh);
            db.SaveChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                BILLINFO ctdh = new BILLINFO();
                ctdh.BILLID = dh.ID;
                ctdh.PRODUCTID = item.iMaSP;
                ctdh.STOCK = item.iSoLuong;
                ctdh.PRICE = (int)item.dDonGia;
                db.BILLINFOes.Add(ctdh);
            }
            db.SaveChanges();
            Session["cart"] = null;
            return RedirectToAction("Index", "Home");
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult customCart()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart_DTO> lstCart = getCart();
            return View(lstCart);

        }
        //Xóa giỏ hàng
        public ActionResult deletCartItem(int iMaSP)
        {
            //Kiểm tra masp
            PRODUCT sp = db.PRODUCTs.SingleOrDefault(n => n.ID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart_DTO> lstGioHang = getCart();
            Cart_DTO sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("customCart");
        }
        //Cập nhật giỏ hàng 
        public ActionResult updateCartItem(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            PRODUCT sp = db.PRODUCTs.SingleOrDefault(n => n.ID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart_DTO> lstGioHang = getCart();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            Cart_DTO sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("cart");
        }
    }
}