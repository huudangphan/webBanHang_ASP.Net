using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyShopEntities db = new QuanLyShopEntities();
        // GET: GioHang
        public List<GioHang> getGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(int masp,string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == masp);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = getGioHang();
            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng hay chưa
            GioHang sanpham = listGioHang.Find(x => x.maSP == masp);
            if (sanpham == null)
            {
                sanpham = new GioHang(masp);
                return Redirect(url);
            }
            else
            {
                sanpham.soLuong++;
                return Redirect(url);
            }

        }
        // cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int masp,FormCollection f)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = getGioHang();
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if(lsp!=null)
            {
                lsp.soLuong = Int32.Parse(f["txtSoLuong"].ToString());
            }
            return View("GioHang");
        }
    }
}