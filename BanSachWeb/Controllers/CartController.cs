using BanSachWeb.Models;
using BanSachWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BanSachWeb.Controllers
{
    public class CartController : Controller
    {
        QuanLyBanSachModel db=new QuanLyBanSachModel();
        // GET: Cart
        public ActionResult Index()
        {
            var username = Session["user"].ToString();
            var account = db.TaiKhoans.Where(s => s.Email == username).FirstOrDefault();
            var cart = GetCart();
            TempData["totalPrice"]=cart.GetTotalPrice();
            var viewModel = new CheckoutViewModel
            {
                TaiKhoan = account,
                GioHang = cart
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(int maSach, int quantity, string type)
        {
            var sach = GetSachById(maSach);
            var totalPrice = sach.GiaBan * quantity;
            var cart = GetCart();
            cart.AddItem(sach, quantity);
            SaveCart(cart);

            // Kiểm tra xem tham số type có giá trị "ajax" hay không
            if (type == "ajax")
            {
                // Trả về một phản hồi JSON nếu yêu cầu là AJAX
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromCart(int maSach)
        {
            var cart = GetCart();
            cart.RemoveItem(maSach);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        private GioHang GetCart()
        {
            var cart = (GioHang)Session["Cart"];
            if (cart == null)
            {
                cart = new GioHang();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCart(GioHang cart)
        {
            Session["Cart"] = cart;
        }

        private Sach GetSachById(int id)
        {
           
                return db.Saches.Find(id);
         
        }

        public int CountItemInCart()
        {
            var cart = GetCart();
            int countItem = 0;
            if (cart != null)
            {
                foreach (var item in cart.GetItems())
                {
                    countItem += item.quantity;
                }
            }
            return countItem;
        }
        public ActionResult CheckOut(string paymentMethod)
        {
            
            var username = Session["user"].ToString();
            var account = db.TaiKhoans.Where(s => s.Email == username).FirstOrDefault();
            var cart = GetCart();
            if (cart == null || !cart.GetItems().Any())
            {
                return RedirectToAction("Index");
            }
            var order = new DonHang
            {
                ThoiGianDatHang = DateTime.Now,
                TrangThai = "Đã tiếp nhận",
                TongGiaTri = cart.GetTotalPrice(),
                MaTaiKhoan = account.MaTaiKhoan,
                PhuongThucThanhToan=paymentMethod,
                ChiTietDonHangs = cart.GetItems().Select(item => new ChiTietDonHang
                {

                    MaSach = item.product.MaSach,
                    SoLuong = item.quantity,
                    GiaBan = item.product.GiaBan,
                    ThanhTien = item.product.GiaBan * item.quantity

                }).ToList()
            };
            db.DonHangs.Add(order);
            db.SaveChanges();
            ClearCart();
            return RedirectToAction("OrderConfirmation", "Cart", new { orderId = order.MaDonHang });
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int maSach, int newQuantity)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu dựa trên mã sách
            var sach = GetSachById(maSach);
            if (sach == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem số lượng mới có hợp lệ không
            if (newQuantity < 1)
            {
                // Trả về một phản hồi lỗi nếu số lượng không hợp lệ
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy giỏ hàng của người dùng từ Session hoặc cơ sở dữ liệu
            var cart = GetCart();

            // Cập nhật số lượng của sản phẩm trong giỏ hàng
            cart.UpdateQuantity(maSach, newQuantity);

            // Lưu lại giỏ hàng đã cập nhật vào Session hoặc cơ sở dữ liệu
            SaveCart(cart);

            // Trả về một phản hồi không có nội dung, chỉ cần thông báo rằng yêu cầu đã được xử lý thành công
            return RedirectToAction("Index");
        }

        public ActionResult OrderConfirmation(int orderId)
        {
            var order = db.DonHangs.Include("ChiTietDonHangs").FirstOrDefault(s => s.MaDonHang == orderId);
            if(order == null)
            {
                return HttpNotFound();
            }
            return View(order);
            
        }
    }
}
