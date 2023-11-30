using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDienThoai.Models
{
    public class Cart_DTO
    {
        WEBDIENTHOAIEntities2 db = new WEBDIENTHOAIEntities2();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get;
            set ;
        }
        public Cart_DTO(int MaSP)
        {
            iMaSP = MaSP;
            PRODUCT sp = db.PRODUCTs.Single(n => n.ID == iMaSP);
            sTenSP = sp.Name;
            sAnhBia = sp.Picture;
            dDonGia = double.Parse(sp.Price.ToString());
            iSoLuong = 1;
            dThanhTien = dDonGia * iSoLuong;
        }
    }
}