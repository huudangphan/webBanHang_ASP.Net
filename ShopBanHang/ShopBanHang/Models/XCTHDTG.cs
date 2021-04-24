using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class XCTHDTG
    {
        public IEnumerable<HDTraGop> hdtragop { get; set; }
        public IEnumerable<CTHDTG> cthdtg { get; set; }
        public IEnumerable<KhachHang> khachang { get; set; }
        public IEnumerable<SanPham> sapham { get; set; }
    }
}