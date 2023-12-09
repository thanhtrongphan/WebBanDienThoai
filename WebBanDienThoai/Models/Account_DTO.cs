using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebBanDienThoai.Models
{
    public class Account_DTO
    {
        public string username;
        public string password;
        public Account_DTO()
        {
            
        }
        public Account_DTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}