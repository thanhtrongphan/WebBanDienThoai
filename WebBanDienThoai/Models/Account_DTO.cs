using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebBanDienThoai.Models
{
    public class Account_DTO
    {
        private string username;
        private string password;
        private string email;
        private string numberphone;
        private string address;
        private Image picture;
        private string type;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string Numberphone { get => numberphone; set => numberphone = value; }
        public string Address { get => address; set => address = value; }
        public Image Picture { get => picture; set => picture = value; }
        public string Type { get => type; set => type = value; }

        public Account_DTO(string username, string password, string email, string numberphone, string address, Image picture, string type)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Numberphone = numberphone;
            this.Address = address;
            this.Picture = picture;
            this.Type = type;
        }
        public Account_DTO()
        {
                
        }
    }
}