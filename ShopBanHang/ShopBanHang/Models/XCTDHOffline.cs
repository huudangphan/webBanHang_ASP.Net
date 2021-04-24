using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class XCTDHOffline
    {
        public IEnumerable<HDOffLine> hdOffline { get; set; }
        public IEnumerable<CTHDOff> cthdOffline { get; set; }
        public IEnumerable<KhachHang> khachang { get; set; }
        public IEnumerable<SanPham> sapham { get; set; }
    }
}