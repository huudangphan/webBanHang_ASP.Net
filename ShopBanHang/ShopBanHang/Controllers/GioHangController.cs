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

namespace ShopBanHang.Controllers
{
    public class GioHangController : Controller
    {

        ShopDoCongNgheEntities2 db = new ShopDoCongNgheEntities2();

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
        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(x => x.thanhTien);
            }
            ViewBag.tongtien = tongTien;
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

            HDOnline hd = new HDOnline();
            List<GioHang> gioHang = getGioHang();
            KhachHang kh = (KhachHang)Session["taiKhoan"];
            hd.MaKH = kh.MaKH;
            hd.NgayDat = DateTime.Now;
            hd.TinhTrang = false;
            
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

            db.SaveChanges();

            return RedirectToAction("DatHangThanhCong", "GioHang");
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TaoCTHDTG(int MaKH)
        {
            HDTG.maKH = MaKH;
            return View();
        }
        [HttpPost]
        public ActionResult TaoCTHDTG(HDTG hd)
        {
            try
            {
                KhachHang kh = db.KhachHangs.Where(x => x.MaKH == HDTG.maKH).SingleOrDefault();
                HDTraGop hdtg = new HDTraGop();
                hdtg.MaKH = HDTG.maKH;
                hdtg.NgayCoc = DateTime.Now;
                hdtg.TienCoc = Convert.ToDecimal(hd.TienCoc);
                hdtg.SoThang = hd.soThang;
                hdtg.laiSuat = hd.laiSuat;
                if (hdtg.SoThang == 3)
                {
                    hdtg.laiSuat = 3;
                }
                else
                {
                    hdtg.laiSuat = 2;
                }
                db.HDTraGops.Add(hdtg);
                List<GioHang> gioHang = getGioHang();
                foreach (var item in gioHang)
                {
                    CTTonKho kho = db.CTTonKhoes.Where(x => x.MaSP == item.maSP && x.MaKho == hd.maKho).SingleOrDefault();
                    kho.SL -= item.soLuong;
                    CTHDTG cthd = new CTHDTG();
                    cthd.MaKho = hd.maKho;
                    cthd.MaHD = hdtg.MaHD;
                    cthd.MaSP = item.maSP;
                    cthd.SL = item.soLuong;
                    cthd.GiaBan = item.donGia;
                    db.CTHDTGs.Add(cthd);
                }
                double tongTien = TongTien();
                double tienHangThang = tongTien / hd.soThang;
                int day = 31;
                int month = 11;
                int year = DateTime.Now.Year;
                int j = 0;
                string time = "";
                for (int i = 1; i <= hd.soThang; i++)
                {
                    PhieuTraGop tg = new PhieuTraGop();
                    tg.MaHD = hdtg.MaHD;
                    tg.Ki = i;
                    tg.MaMucPhat = 1;
                    tg.TienDong = (decimal?)tienHangThang;
                    tg.TienPhat = 0;
                    j = month + i;
                    if (j > 12)
                    {
                        year = 2022;
                        int count = hd.soThang - i;
                        for (int a = 1; a < count + 2; a++)
                        {
                            time = year + "-" + a + "-" + day;
                            if (thang30(a) && day == 31)
                            {
                                time = year + "-" + a + "-30";
                            }
                            if (a == 2 && day > 29)
                            {
                                time = year + "-" + a + "-28";
                            }
                            tg.NgayDenHan = DateTime.Parse(time);
                            db.PhieuTraGops.Add(tg);
                        }
                    }
                    else
                    {
                        time = year + "-" + j + "-" + day;
                        if (thang30(j) && day == 31)
                        {
                            time = year + "-" + j + "-30";
                        }
                        if (j == 2 && day > 29)
                        {
                            time = year + "-" + j + "-28";
                        }

                        tg.NgayDenHan = DateTime.Parse(time);
                        db.PhieuTraGops.Add(tg);
                    }





                }
                db.SaveChanges();
                return RedirectToAction("Index", "QuanLy");
            }
            catch (Exception)
            {

                ViewBag.Error = "Tiền cọc ít nhất phải bằng 1 nửa giá trị đơn hàng";
            }
          

            return View();
        }

        [HttpGet]
        public ActionResult TaoCTHDOff(int MaKH)
        {
            HDOff.MaKH = MaKH;
            return View();
        }
        [HttpPost]
        public ActionResult TaoCTHDOff(HDOff hd)
        {
           
            HDOffLine hdOff = new HDOffLine();
            hdOff.MaKH = HDOff.MaKH;
            hdOff.NgayMua = DateTime.Now;
            db.HDOffLines.Add(hdOff);

            List<GioHang> gioHang = getGioHang();

            foreach (var item in gioHang)
            {
                CTTonKho lstKho = db.CTTonKhoes.Where(x => x.MaKho == hd.maKho && x.MaSP == item.maSP).SingleOrDefault();
                lstKho.SL -= item.soLuong;
                CTHDOff cthd = new CTHDOff();
                cthd.MaHD = hdOff.MaHD;
                cthd.MaKho = hd.maKho;
                cthd.MaSP = item.maSP;
                cthd.SL = item.soLuong;
                cthd.GiaBan = item.donGia;
                db.CTHDOffs.Add(cthd);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "QuanLy");
        }

        public bool thang30(int month)
        {
            int[] m = new int[] { 4, 6, 9, 11 };
            for(int i = 0; i < m.Length; i++)
            {
                if (month == m[i])
                    return true;
            }
            return false;
        }
        
    }
}