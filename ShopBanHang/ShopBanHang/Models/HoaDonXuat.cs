using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class HoaDonXuat
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();
        public static int mahd { get; set; }
        public int masp { get; set; }
        public string tenSP { get; set; }
        public int sl { get; set; }
        public double donGia { get; set; }
        public double thanhTien { get { return sl * donGia; } }
        public DateTime ngay { get; set; }
        public string tenkh { get; set; }
        public string diaChi { get; set; }
        public int sdt { get; set; }

    }
}