using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { get; set; }
    }
}