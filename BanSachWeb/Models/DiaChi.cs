namespace BanSachWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiaChi")]
    public partial class DiaChi
    {
        [Key]
        public int MaDiaChi { get; set; }

        [StringLength(100)]
        public string QuocGia { get; set; }

        [StringLength(100)]
        public string Tinh { get; set; }

        [StringLength(100)]
        public string Huyen { get; set; }

        [StringLength(100)]
        public string Xa { get; set; }

        [Column(TypeName = "text")]
        public string DiaChiCuThe { get; set; }

        public bool? MacDinh { get; set; }

        [StringLength(15)]
        public string SoDienThoaiNhanHang { get; set; }

        [StringLength(255)]
        public string TenNguoiNhan { get; set; }

        public int? MaTaiKhoan { get; set; }
    }
}
