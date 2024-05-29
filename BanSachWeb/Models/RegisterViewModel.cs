using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanSachWeb.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pass { get; set; }
    }
}



