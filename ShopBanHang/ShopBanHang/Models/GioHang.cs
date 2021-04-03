using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class GioHang
    {
        QLShopEntities db = new QLShopEntities();
        //databaseEntities db = new databaseEntities();
        public int maSP { get; set; }
        public string tenSP { get; set; }
        public string hinhAnh { get; set; }
        public int donGia { get; set; }
        public int soLuong { get; set; }
        public double thanhTien { get { return soLuong * donGia; } }
        public GioHang(int imaSP)
        {
            maSP = imaSP;
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == maSP);
            tenSP = sp.tenSP;
            hinhAnh = sp.anh;
            donGia = (int)sp.giaSP;
            soLuong = 1;
        }

    }
}