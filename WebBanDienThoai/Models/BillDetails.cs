using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDienThoai.Models
{
    public class BillDetails
    {
        public List<Cart_DTO> lstCart { get; set; }
        public BILL bill { get; set; }
    }
}