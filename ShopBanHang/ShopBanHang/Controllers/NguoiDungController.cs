using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using ShopBanHang.MultiData;

namespace ShopBanHang.Controllers
{
    public class NguoiDungController : Controller
    {
        ShopDoCongNgheEntities1 db = new ShopDoCongNgheEntities1();
        //databaseEntities db = new databaseEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult DangKy(KhachHang kh)
        //{
        //    var check = db.KhachHangs.SingleOrDefault(c => c.taiKhoan == kh.taiKhoan);
        //    if (check == null)
        //    {


        //        return View();
        //    }

        //    else
        //    {
        //        ViewBag.Error = "Tài khoản đã tồn tại";
        //        return View();
        //    }


        //}

        [HttpGet]
        public ActionResult DangKy2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy2(ModelUser kh)
        {
            //var check = db.KhachHangs.SingleOrDefault(c => c.taiKhoan == kh.username);
            try
            {
                KhachHang khach = new KhachHang();
                khach.tenKH = kh.tenKH;
                khach.DiaChi = kh.diaChi;
                khach.email = kh.email;
                khach.SDT = kh.sdt;

                khach.taiKhoan = kh.username;
                khach.matKhau = kh.password;
                db.KhachHangs.Add(khach);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            catch (Exception ex)
            {

                ViewBag.thongBao = "Tài khoản bị trùng";
                return View();
            }

            //if (check == null && kh.password == kh.confirmPassword)
            //{
            //    KhachHang khach = new KhachHang();
            //    khach.tenKH = kh.tenKH;
            //    khach.DiaChi = kh.diaChi;
            //    khach.email = kh.email;
            //    khach.SDT = kh.sdt;

            //    khach.taiKhoan = kh.username;
            //    khach.matKhau = kh.password;
            //    db.KhachHangs.Add(khach);
            //    db.SaveChanges();
            //    return RedirectToAction("DangNhap", "NguoiDung");
            //}

            //else
            //{

            //    return View();
            //}


        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(KhachHang kh)
        {

            var data = db.KhachHangs.Where(c => c.taiKhoan.Equals(kh.taiKhoan) && c.matKhau.Equals(kh.matKhau)).ToList();
            var khachhang = db.KhachHangs.SingleOrDefault(n => n.taiKhoan == kh.taiKhoan && n.matKhau == kh.matKhau);
            
             if (data.Count() > 0)
            {
                Session["taiKhoan"] = khachhang;
                Session["tk"] = data.FirstOrDefault().taiKhoan;
                Session["tenKH"] = data.FirstOrDefault().tenKH;

                Session["matKhau"] = data.FirstOrDefault().matKhau;
                Session["diaChi"] = data.FirstOrDefault().DiaChi;

                Session["email"] = data.FirstOrDefault().email;
                Session["sdt"] = data.FirstOrDefault().SDT;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu sai";
                return View();
            }

        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HoSoNguoiDung()
        {
            string tenTK = @Session["tk"].ToString();
            var kh = db.KhachHangs.Where(x => x.taiKhoan == tenTK).SingleOrDefault();
            return View(kh);
        }
        //public ActionResult AdminTT()
        //{
        //    return View();
        //}
        //public ActionResult DonHang()
        //{
        //    return View();
        //}
        //public ActionResult Hang()
        //{
        //    string taikhoandn = Session["tk"].ToString();
        //    var mymodel = new MultiDataa();

        //    mymodel.donhang = (from a in db.KhachHangs
        //                       join b in db.DonHangs
        //                       on a.maKH equals b.maKH
        //                       where a.taiKhoan == taikhoandn
        //                       select b).ToList();



        //    return View(mymodel);
        //}
        //public ActionResult XemChiTietDonHang(int madh)
        //{
        //    var mymodel = new MultiDataa();
        //    mymodel.donhang = db.DonHangs.Where(x => x.maDH == madh).ToList();
        //    mymodel.ctdonhang = db.ChiTietDonHangs.Where(x => x.maDH == madh).ToList();
        //    var makh = (from c in mymodel.donhang
        //                where c.maDH == madh
        //                select c.maKH).FirstOrDefault();

        //    mymodel.khachhang = db.KhachHangs.Where(c => c.maKH == makh);
        //    mymodel.sanPhams = (from a in db.ChiTietDonHangs
        //                        join b in db.SanPhams
        //                        on a.maSP equals b.maSP
        //                        where a.maDH == madh
        //                        select b).ToList();

        //    return View(mymodel);
        //}
    }
}