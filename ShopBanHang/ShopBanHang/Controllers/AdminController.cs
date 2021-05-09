using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class AdminController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin add)
        {
            string username = add.userAdmin.ToString();
            string password = add.passAdmin.ToString();
            var data = db.Admins.Where(x => x.userAdmin == username && x.passAdmin == password).FirstOrDefault();
            if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                ViewBag.Loi1 = "Tên đăng nhập và mật khẩu không được để trống";
            }
            else
            {
                Admin ad = db.Admins.Where(x => x.userAdmin == username && x.passAdmin == password).SingleOrDefault();
                if (ad != null)
                {
                    Session["tenKH"] = data.tenAdmin;
                    if (username == "Admin")
                        return RedirectToAction("Index2", "QuanLy");
                  
                    return RedirectToAction("Index", "QuanLy");
                }
                else
                    ViewBag.Loi2 = "Tên đăng nhập hoặc mật khẩu sai";
            }        
            
                       

            return View();

        }
        public ActionResult QLTKNV(string tenNV)
        {

            return View(db.Admins.Where(x=>x.tenAdmin.Contains(tenNV)).ToList());

        }
        public ActionResult QTTKKH(string tenKH)
        {
            return View(db.KhachHangs.Where(x=>x.tenKH.Contains(tenKH)).ToList());
        }
        [HttpGet]
        public ActionResult CTNV(string user)
        {
            Admin ad = db.Admins.Where(x => x.userAdmin == user).SingleOrDefault();
            return View(ad);
        }
        [HttpPost]
        public ActionResult CTNV(Admin ad)
        {
            var nv = db.Admins.Where(x => x.userAdmin == ad.userAdmin).FirstOrDefault();
            nv.tenAdmin = ad.tenAdmin;
            nv.passAdmin = ad.passAdmin;
            db.SaveChanges();

            return RedirectToAction("Index2", "QuanLy");
        }
        
        public ActionResult CTKH(int makh)
        {
            KhachHang kh = db.KhachHangs.Where(x => x.MaKH == makh).SingleOrDefault();
            return View(kh);
        }
        public ActionResult TaoHDNhap(string ma)
        {
            return View(db.SanPhams.Where(x=>x.tenSP.Contains(ma)).ToList());
        }
        [HttpGet]
        public ActionResult addKho()
        {
            ViewBag.maKho = new SelectList(db.Khoes.ToList(), "maKho", "tenKho");
            return View();
        }
        [HttpPost]
        public ActionResult addKho(CTTonKho k)
        {
            if(ModelState.IsValid)
            {
                CTTonKho kho = new CTTonKho();
                SanPham sp = db.SanPhams.Where(x => x.tenSP == CTKho.ten && x.anh == CTKho.anh).SingleOrDefault();

                kho.MaSP = sp.maSP;
                kho.MaKho = k.MaKho;
                kho.SL = k.SL;
                if(kho.SL<=0)
                {

                }
                else
                {
                    db.CTTonKhoes.Add(kho);
                    db.SaveChanges();
                }
            }
            ViewBag.maKho = new SelectList(db.Khoes.ToList(), "maKho", "tenKho");
            return RedirectToAction("Index2", "QuanLy");
        }
    }
}