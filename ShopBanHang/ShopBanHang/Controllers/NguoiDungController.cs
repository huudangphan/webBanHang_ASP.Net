using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;


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
                if (ModelState.IsValid)
                {
                    KhachHang khach = new KhachHang();
                    khach.tenKH = kh.tenKH;
                    khach.DiaChi = kh.diaChi;
                    khach.email = kh.email;
                    khach.sodt = kh.sdt;

                    khach.taiKhoan = kh.username;
                    khach.matKhau = kh.password;
                    db.KhachHangs.Add(khach);
                    db.SaveChanges();
                    return RedirectToAction("DangNhap", "NguoiDung");
                }
                return View();
                
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
            if (kh.taiKhoan == null || kh.matKhau == null)
            {
                ViewBag.error = "Tài khoản và mật khẩu không thể để trống";
                return View();
            }
            else
            {
                var data = db.KhachHangs.Where(c => c.taiKhoan.Equals(kh.taiKhoan) && c.matKhau.Equals(kh.matKhau)).ToList();
                var khachhang = db.KhachHangs.SingleOrDefault(n => n.taiKhoan == kh.taiKhoan && n.matKhau == kh.matKhau);

                if (data.Count() > 0)
                {
                    Session["taiKhoan"] = khachhang;
                    Session["tk"] = data.FirstOrDefault().taiKhoan;
                    Session["tenKH"] = data.FirstOrDefault().tenKH;
                    Session["MaKH"] = data.FirstOrDefault().MaKH;
                    Session["matKhau"] = data.FirstOrDefault().matKhau;
                    Session["diaChi"] = data.FirstOrDefault().DiaChi;

                    Session["email"] = data.FirstOrDefault().email;
                    Session["sdt"] = data.FirstOrDefault().sodt;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu sai";
                    return View();
                }
            }
            

        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult HoSoNguoiDung()
        {
            string tenTK = @Session["tk"].ToString();        

            var kh = db.KhachHangs.Where(x => x.taiKhoan == tenTK).SingleOrDefault();
            return View(kh);
        }
        [HttpPost]
        public ActionResult HoSoNguoiDung(FormCollection f)
        {
            string curPass = f["cur_pass"].ToString();
            string newPass = f["pass"].ToString();
            string conPass = f["pass2"].ToString();
            string tenTK = @Session["tk"].ToString();
            if (string.IsNullOrEmpty(curPass)||string.IsNullOrEmpty(newPass)||string.IsNullOrEmpty(conPass))
            {
                ViewBag.error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            if(newPass!=conPass)
            {
                ViewBag.error = "Mật khẩu mới và xác nhận mật khẩu phải giống nhau";
                return View();
            }
            else
            {
                try
                {
                    var kh = db.KhachHangs.Where(x => x.taiKhoan == tenTK && x.matKhau == curPass).SingleOrDefault();
                    if(kh!=null)
                    {
                        kh.matKhau = newPass;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }   
                    else
                    {
                        ViewBag.error = "Mật khẩu hiện tại sai";
                        return View();
                    }    
                   
                }
                catch (Exception)
                {

                    throw;
                }
               
            }
            return View();
           
        }
        public ActionResult LichSuDonHang(int makh)
        {
            var list = db.HDOnlines.Where(x => x.MaKH == makh).ToList();
            return View(list);

        }
        public ActionResult ChiTietDonHang(int madh)
        {
            var list = db.CTHDOnlines.Where(x => x.MaHD == madh).ToList();
            return View(list);
        }
        public void Chon(int id)
        {
            user.id = id;
            RedirectToAction("KhachHang", "NhanVien");
        }


    }
}