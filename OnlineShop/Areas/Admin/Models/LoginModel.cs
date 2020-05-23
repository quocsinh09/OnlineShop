using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Moi nhap ....")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "pass.....")]
        public string Password { get; set; }
    }
}