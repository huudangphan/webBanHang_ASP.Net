﻿using System;
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
        QLShopEntities db = new QLShopEntities();
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
            int makh = (from c in db.KhachHangs
                        where c.taiKhoan == taikhoandn
                        select c.maKH).SingleOrDefault();
            int madh = (from a in db.DonHangs
                        where a.maKH == makh
                        select a.maDH).FirstOrDefault();
            mymodel.donhang = db.DonHangs.Where(x => x.maDH == madh).ToList();
            mymodel.ctdonhang = db.ChiTietDonHangs.Where(x => x.maDH == madh).ToList();
            
            return View(mymodel);
        }
    }
}