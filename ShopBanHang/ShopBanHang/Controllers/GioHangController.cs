using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using System.IO;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;

namespace ShopBanHang.Controllers
{
    public class GioHangController : Controller
    {

        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

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
        public ActionResult CapNhatGioHangAdmin(int masp, FormCollection f)
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
                lsp.donGia = Int32.Parse(f["dongia"].ToString());
            }

            return RedirectToAction("GioHangAdmin");
        }
        // Xóa giỏ hàng
        public  ActionResult XoaGioHang(int masp)
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
        public static void XoaGH(int masp, List<GioHang> listGioHang)
        {
                      
            GioHang lsp = listGioHang.SingleOrDefault(x => x.maSP == masp);
            if (lsp != null)
            {
                listGioHang.RemoveAll(x => x.maSP == masp);

            }
        }
        // Giao diện giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public ActionResult GioHangAdmin()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("SanPham", "NhanVien");
            List<GioHang> listGioHang = getGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public PartialViewResult PartialGioHang()
        {
            
            List<GioHang> listGioHang = getGioHang();

            return PartialView(listGioHang);
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
        public double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(x => x.thanhTien);
            }
            ViewBag.tongtien = tongTien;
            user.tongtien = tongTien;
            return tongTien;
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            List<GioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }
        public ActionResult SuaGioHangAdmin()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("SanPham", "NhanVien");
            List<GioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }


        [HttpPost]
        public ActionResult DatHang(int type=0)
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

            HDOnline hd = new HDOnline();
            List<GioHang> gioHang = getGioHang();
            KhachHang kh = (KhachHang)Session["taiKhoan"];
            hd.MaKH = kh.MaKH;
            hd.NgayDat = DateTime.Now;
            if(type==1)
            {
                hd.TinhTrang = true;
            }
            else
            {
                hd.TinhTrang = false;
            }
          
            
            db.HDOnlines.Add(hd);
            foreach (var item in gioHang)
            {
                CTHDOnline cthd = new CTHDOnline();
                cthd.MaKho = 1;
                cthd.SL = item.soLuong;
                cthd.MaHD = hd.MaHD;
                cthd.MaSP = item.maSP;
                cthd.GiaBan = item.donGia;
                db.CTHDOnlines.Add(cthd);

            }
            Session["GioHang"] = null;
            db.SaveChanges();

            return RedirectToAction("DatHangThanhCong", "GioHang");
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
        public ActionResult NhapHang()
        {
            return View();
        }

    }
}