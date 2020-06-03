using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu không chính xác")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [Compare("CurrentPassword", ErrorMessage = "Mật khẩu mới phải khác mật khẩu hiện tại")]
        [StringLength(50,ErrorMessage = "Mật khẩu  mới phải có ít nhất {2} ký  tự và tối đa {0} ký tự")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.Vui lòng  kiểm tra lại")]
        public string ConfirmPassword { get; set; }
    }
}
