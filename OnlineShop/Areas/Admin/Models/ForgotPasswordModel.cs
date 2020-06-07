using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Xin mời nhập Email tài khoản của bạn")]
        public string Email { get; set; }
    }
}