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
        //QLShopEntities db = new QLShopEntities();
        databaseEntities db = new databaseEntities();
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
        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            var check = db.KhachHangs.FirstOrDefault(c => c.taiKhoan == kh.taiKhoan);
            if (check == null)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                
                return View();
            }

            else
            {
                ViewBag.Error = "Tài khoản đã tồn tại";
                return View();
            }
            
           
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
            if(data.Count()>0&&kh.taiKhoan=="admin")
            {
                Session["taiKhoan"] = khachhang;
                Session["tk"] = data.FirstOrDefault().taiKhoan;
                Session["tenKH"] = data.FirstOrDefault().tenKH;
               
                Session["matKhau"] = data.FirstOrDefault().matKhau;
                Session["diaChi"] = data.FirstOrDefault().diaChi;
                Session["ngaySinh"] = data.FirstOrDefault().ngaySinh;
                Session["email"] = data.FirstOrDefault().emai;
                Session["sdt"] = data.FirstOrDefault().sdt;
                return RedirectToAction("Index", "QuanLy");
            }    
            else if(data.Count()>0)
            {
                Session["taiKhoan"] = khachhang;
                Session["tk"] = data.FirstOrDefault().taiKhoan;
                Session["tenKH"] = data.FirstOrDefault().tenKH;

                Session["matKhau"] = data.FirstOrDefault().matKhau;
                Session["diaChi"] = data.FirstOrDefault().diaChi;
                Session["ngaySinh"] = data.FirstOrDefault().ngaySinh;
                Session["email"] = data.FirstOrDefault().emai;
                Session["sdt"] = data.FirstOrDefault().sdt;
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
            return RedirectToAction("Index","Home");
        }
      
        public ActionResult HoSoNguoiDung()
        {
            

            return View();
        }
        public ActionResult AdminTT()
        {
            return View();
        }
        public ActionResult DonHang()
        {
            return View();
        }
        public ActionResult Hang()
        {
            string taikhoandn = Session["tk"].ToString();
            var mymodel = new MultiDataa();
       
            mymodel.donhang = (from a in db.KhachHangs
                               join b in db.DonHangs
                               on a.maKH equals b.maKH
                               where a.taiKhoan == taikhoandn
                               select b).ToList();
            //mymodel.ctdonhang = (from a in db.ChiTietDonHangs
            //                    join b in db.DonHangs
            //                    on a.maDH equals b.maDH
            //                    join c in db.SanPhams
            //                    on a.maSP equals c.maSP
            //                    where b.maKH==makh
            //                    select a).ToList();
                                


            return View(mymodel);
        }
        public ActionResult XemChiTietDonHang(int madh)
        {
            var mymodel = new MultiDataa();
            mymodel.donhang = db.DonHangs.Where(x => x.maDH == madh).ToList();
            mymodel.ctdonhang = db.ChiTietDonHangs.Where(x => x.maDH == madh).ToList();
            var makh = (from c in mymodel.donhang
                        where c.maDH == madh
                        select c.maKH).FirstOrDefault();

            mymodel.khachhang = db.KhachHangs.Where(c => c.maKH == makh);
            mymodel.sanPhams = (from a in db.ChiTietDonHangs
                                join b in db.SanPhams
                                on a.maSP equals b.maSP
                                where a.maDH == madh
                                select b).ToList();






            return View(mymodel);
        }
    }
}