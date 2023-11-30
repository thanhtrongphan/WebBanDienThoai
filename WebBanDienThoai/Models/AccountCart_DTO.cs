using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDienThoai.Models
{
    public class AccountCart_DTO
    {
        private List<Cart_DTO> lstCart;
        private Account account;

        public AccountCart_DTO(List<Cart_DTO> lstCart, Account account)
        {
            this.lstCart = lstCart;
            this.account = account;
        }
        public AccountCart_DTO()
        {

        }
        public List<Cart_DTO> LstCart { get => lstCart; set => lstCart = value; }
        public Account Account { get => account; set => account = value; }
    }
}