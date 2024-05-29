using BanSachWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSachWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Main
        private QuanLyBanSachModel db=new QuanLyBanSachModel();

        // GET: TaiKhoans
        public ActionResult Index()
        {
            return View(db.TaiKhoans.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]

        public ActionResult Login(string user, string pass)
        {
            
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                ViewBag.err = "Tên đăng nhập và mật khẩu không được để trống!";
                return View("Login");
            }
            var a = db.TaiKhoans.Where(p => (p.Email == user || p.SoDienThoai == user) && p.MatKhau == pass).FirstOrDefault();
            if (a != null)
            {
                Session["user"] = a.Email;
                return RedirectToAction("MainContent", "Home");
            }
            else
            {
                ViewBag.err = "Sai tên đăng nhập hoặc mật khẩu!";
                return View("Login");
            }
            

        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        // POST: /Account/SendOtp
        [HttpPost]
        public ActionResult SendOtp(string emailOrPhoneNumber)
        {
            // Kiểm tra xem email hoặc số điện thoại có tồn tại trong cơ sở dữ liệu không
            var user = db.TaiKhoans.FirstOrDefault(u => u.Email == emailOrPhoneNumber || u.SoDienThoai == emailOrPhoneNumber);
            if (user == null)
            {
                ViewBag.Error = "Email hoặc số điện thoại không tồn tại.";
                return View("ForgetPass");
            }

            // Sinh mã OTP và gửi nó đến email hoặc số điện thoại của người dùng (bạn có thể sử dụng thư viện gửi email hoặc SMS tương ứng)

            TempData["UserId"] = user.MaTaiKhoan;
            TempData["OtpCode"] = "123456"; // Thay "123456" bằng mã OTP thực tế

            return RedirectToAction("VerifyOtp");
        }

        // GET: /Account/VerifyOtp
        public ActionResult VerifyOtp()
        {
            return View();
        }

        // POST: /Account/VerifyOtp
        [HttpPost]
        public ActionResult VerifyOtp(string otp)
        {
            var userId = (int)TempData["UserId"];
            var savedOtp = TempData["OtpCode"].ToString();

            if (otp != savedOtp)
            {
                ViewBag.Error = "Mã OTP không hợp lệ.";
                return View("VerifyOtp");
            }

            return RedirectToAction("SetNewPassword");
        }

        // GET: /Account/SetNewPassword
        public ActionResult SetNewPassword()
        {
            return View();
        }

        // POST: /Account/SetNewPassword
        [HttpPost]
        public ActionResult SetNewPassword(string newPassword)
        {
            var userId = (int)TempData["UserId"];
            var user = db.TaiKhoans.Find(userId);

            if (user == null)
            {
                ViewBag.Error = "Người dùng không tồn tại.";
                return View("SetNewPassword");
            }

            // Cập nhật mật khẩu mới cho người dùng
            user.MatKhau = newPassword;
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string oldPass, string newPass, string confirmPass)
        {
            if(string.IsNullOrWhiteSpace(oldPass)|| string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                ViewBag.mess = "Vui lòng nhập đầy đủ thông tin!";
                return View("ChangePass");
            }
            if (Session["user"] != null)
            {
                var email = Session["user"].ToString();
                var user = db.TaiKhoans.SingleOrDefault(p => p.Email == email);
                if (user != null)
                {
                    if (user.MatKhau == oldPass && newPass == confirmPass && newPass != oldPass)
                    {
                        user.MatKhau = newPass;
                        db.SaveChanges();
                        return RedirectToAction("MainContent", "Home");
                    }
                    else if (user.MatKhau != oldPass)
                    {
                        ViewBag.mess = "Nhập không đúng mật khẩu cũ!";
                    }
                    else if (newPass != confirmPass)
                    {
                        ViewBag.mess = "Nhập sai xác nhận mật khẩu!";
                    }
                    else if (newPass == oldPass)
                    {
                        ViewBag.mess = "Mật khẩu mới trùng với mật khẩu cũ!";
                    }
                }
            }
            return View("ChangePass");
        }
        [HttpGet]
        public ActionResult UpdateAccountInfo()
        {
            if (Session["user"] != null)
            {
                string userEmail = Session["user"].ToString();
                var user = db.TaiKhoans.FirstOrDefault(p => p.Email == userEmail);
                if (user != null)
                {
                    // Trả về view với dữ liệu của người dùng để hiển thị trong các ô nhập liệu
                    return View(user);
                }
            }
            // Nếu không tìm thấy thông tin người dùng hoặc người dùng chưa đăng nhập, chuyển hướng đến trang lỗi
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult UpdateAccountInfo(TaiKhoan a)
        {
            if (Session["user"] != null)
            {
                string userEmail = Session["user"].ToString();
                var user = db.TaiKhoans.FirstOrDefault(p => p.Email == userEmail);
                if (user != null)
                {
                    // Cập nhật thông tin người dùng từ dữ liệu gửi đi từ form
                    user.TenDangNhap = a.TenDangNhap;
                    user.Email = a.Email;
                    user.HoTen = a.HoTen;
                    user.SoDienThoai = a.SoDienThoai;
                    db.SaveChanges();
                    return RedirectToAction("MainContent", "Home");
                }
            }
            // Nếu không tìm thấy thông tin người dùng hoặc người dùng chưa đăng nhập, chuyển hướng đến trang lỗi
            return RedirectToAction("Error", "Home");
        }
        public ActionResult UpdateAddress()
        {
            var username = Session["user"].ToString();
            var user = db.TaiKhoans.FirstOrDefault(p => p.Email == username);

            if (user != null)
            {
                var addresses = db.DiaChis.Where(a => a.MaTaiKhoan == user.MaTaiKhoan).ToList();
                return View(addresses);
            }

            return View(new List<DiaChi>());
        }
        [HttpGet]
        public ActionResult CreateAddress()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddress(DiaChi diaChi)
        {
            if (ModelState.IsValid)
            {
                // Get the logged-in user's email from session
                var username = Session["user"].ToString();
                var user = db.TaiKhoans.FirstOrDefault(p => p.Email == username);

                if (user != null)
                {
                    diaChi.MaTaiKhoan = user.MaTaiKhoan; // Assign the user ID to the address
                    db.DiaChis.Add(diaChi);
                    db.SaveChanges();
                    return RedirectToAction("UpdateAddress");
                }
            }
            return View(diaChi);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool emailExist = db.TaiKhoans.Any(p=>p.Email == model.Email);
                if(emailExist) 
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại!");
                    return View(model);
                }
                var newUser = new TaiKhoan
                {
                    Email = model.Email,
                    MatKhau = model.Pass
                };
                db.TaiKhoans.Add(newUser);
                db.SaveChanges();
                return RedirectToAction("MainContent", "Home");
            }
            return View(model);
        }
        public TaiKhoan GetAccount()
        {
            var user = db.TaiKhoans.Where(s => s.TenDangNhap == Session["user"].ToString()).FirstOrDefault();
            return user;
        }
    }



}
