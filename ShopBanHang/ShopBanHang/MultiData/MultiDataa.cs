using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopBanHang.Models;

namespace ShopBanHang.MultiData
{
    public class MultiDataa
    {
        public IEnumerable<DonHang> donhang { get; set; }
        public IEnumerable<ChiTietDonHang> ctdonhang { get; set; }
        public IEnumerable<SanPham> sanPhams { get; set; }
        public IEnumerable<KhachHang> khachhang { get; set; }

    }
}