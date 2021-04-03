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

        QLShopEntities db = new QLShopEntities();
        //databaseEntities db = new databaseEntities();
        
        // GET: GioHang
        public List<GioHang> getGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }// nếu chưa có giỏ hàng
            return listGioHang;
        }
        public ActionResult ThemGioHang(int masp, string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == masp);
            if (sp == null)
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
                // add sản phẩm mới
                listGioHang.Add(sanpham);
                return Redirect(url);
            }
            else
            {
                sanpham.soLuong++;
                return Redirect(url);
            }

        }
        // cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(a => a.maSP == masp);
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng hay không
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = getGioHang();
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                lsp.soLuong = Int32.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("GioHang");
        }
        // Xóa giỏ hàng
        public ActionResult XoaGioHang(int masp)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> listGioHang = getGioHang();
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                listGioHang.RemoveAll(x => x.maSP == masp);

            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        // Giao diện giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }
        // tính tổng số lượng và tổng tiền 
        private int TongSoLuong()
        {
            int tongSL = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongSL = listGioHang.Sum(x => x.soLuong);
            }
            return tongSL;

        }
        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(x => x.thanhTien);
            }
            return tongTien;
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }
        [HttpPost]
        public ActionResult DatHang()
        {
            // Kiểm tra đăng nhập
            if (Session["taiKhoan"] == null || Session["taiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm đơn hàng
            DonHang dh = new DonHang();
            List<GioHang> giohang = getGioHang();
            KhachHang kh = (KhachHang)Session["taiKhoan"];
            dh.maKH = kh.maKH;
            dh.ngayDat = DateTime.Now;
            dh.tinhTrang = 0;
            dh.daThanhToan = 0;
            db.DonHangs.Add(dh);
            db.SaveChanges();
            // Thêm chi tiết đơn hàng
            foreach (var item in giohang)
            {
                ChiTietDonHang ctdonHang = new ChiTietDonHang();

                ctdonHang.maDH = dh.maDH;
                ctdonHang.soLuong = item.soLuong;
                ctdonHang.donGia = item.donGia * item.soLuong;
                ctdonHang.maSP = item.maSP;
                db.ChiTietDonHangs.Add(ctdonHang);
               

            }
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }
    }
}