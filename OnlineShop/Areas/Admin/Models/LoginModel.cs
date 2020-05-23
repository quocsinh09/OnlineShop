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
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string UserName { get; set; }

        [StringLength(50, ErrorMessage = "Mật khẩu phải có tối thiểu {2} ký tự và tối đa {0} ký tự" , MinimumLength = 6)]
        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { get; set; }
    }
}