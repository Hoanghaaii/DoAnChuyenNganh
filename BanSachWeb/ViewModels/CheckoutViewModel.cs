using BanSachWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSachWeb.ViewModels
{
    public class CheckoutViewModel
    {
        public GioHang GioHang { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}