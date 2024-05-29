using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BanSachWeb.Models
{
    public partial class QuanLyBanSachModel : DbContext
    {
        public QuanLyBanSachModel()
            : base("name=QuanLyBanSachModel")
        {
        }

        public virtual DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<DanhMucChinh> DanhMucChinhs { get; set; }
        public virtual DbSet<DanhMucPhu> DanhMucPhus { get; set; }
        public virtual DbSet<DiaChi> DiaChis { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiNhanh>()
                .Property(e => e.TenChiNhanh)
                .IsUnicode(false);

            modelBuilder.Entity<ChiNhanh>()
                .Property(e => e.DiaChi)
                .IsUnicode(false);

            modelBuilder.Entity<ChiNhanh>()
                .Property(e => e.GioiThieu)
                .IsUnicode(false);

            modelBuilder.Entity<ChiNhanh>()
                .HasMany(e => e.Saches)
                .WithMany(e => e.ChiNhanhs)
                .Map(m => m.ToTable("Sach_ChiNhanh").MapLeftKey("MaChiNhanh").MapRightKey("MaSach"));

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.GiaBan)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DanhMucChinh>()
                .Property(e => e.TenDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucChinh>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucPhu>()
                .Property(e => e.TenDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucPhu>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.QuocGia)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.Tinh)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.Huyen)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.Xa)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.DiaChiCuThe)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.SoDienThoaiNhanHang)
                .IsUnicode(false);

            modelBuilder.Entity<DiaChi>()
                .Property(e => e.TenNguoiNhan)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TrangThai)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.DonViVanChuyen)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.PhuongThucThanhToan)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.MaQR)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.PhiVanChuyen)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongGiaTri)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.LoiNhuan)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.KhuyenMais)
                .WithMany(e => e.DonHangs)
                .Map(m => m.ToTable("DonHang_KhuyenMai").MapLeftKey("MaDonHang").MapRightKey("MaKhuyenMai"));

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.MucGiam)
                .HasPrecision(5, 2);

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.DieuKienApDung)
                .IsUnicode(false);

            modelBuilder.Entity<KhuyenMai>()
                .HasMany(e => e.Saches)
                .WithMany(e => e.KhuyenMais)
                .Map(m => m.ToTable("KhuyenMai_Sach").MapLeftKey("MaKhuyenMai").MapRightKey("MaSach"));

            modelBuilder.Entity<KhuyenMai>()
                .HasMany(e => e.TaiKhoans)
                .WithMany(e => e.KhuyenMais)
                .Map(m => m.ToTable("KhuyenMai_TaiKhoan").MapLeftKey("MaKhuyenMai").MapRightKey("MaTaiKhoan"));

            modelBuilder.Entity<PhanHoi>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.TenSach)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.AnhSach)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.GiaGoc)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sach>()
                .Property(e => e.GiaBan)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sach>()
                .Property(e => e.TomTat)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.NhaXuatBan)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.HinhThuc)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.KichThuoc)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.TacGias)
                .WithMany(e => e.Saches)
                .Map(m => m.ToTable("TacGia_Sach").MapLeftKey("MaSach").MapRightKey("MaTacGia"));

            modelBuilder.Entity<TacGia>()
                .Property(e => e.TenTacGia)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .Property(e => e.AnhMinhHoa)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.HoTen)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.VaiTro)
                .IsUnicode(false);
        }
    }
}
