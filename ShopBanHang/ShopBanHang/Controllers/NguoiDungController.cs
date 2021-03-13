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
        QuanLyShopEntities db = new QuanLyShopEntities();
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
            if(data.Count()>0)
            {
                Session["tenKH"] = data.FirstOrDefault().tenKH;
                Session["taiKhoan"] = data.FirstOrDefault().taiKhoan;
                Session["matKhau"] = data.FirstOrDefault().matKhau;
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
    }
}