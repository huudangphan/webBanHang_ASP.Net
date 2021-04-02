using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class HomeController : Controller
    {
        //QLShopEntities db = new QLShopEntities();
        databaseEntities db = new databaseEntities();
        public ActionResult Index()
        {
            var listsp = db.SanPhams.Take(30).ToList();
            var list = from c in db.SanPhams

                       select c;

            return View(listsp);
        }
       public ActionResult SanPhamHot()
        {

            var list = db.SanPhams.OrderBy(c => c.slTon).Where(c=>c.loaiSP=="phone"||c.loaiSP=="laptop").Take(4).ToList();
            var listsp = list.ToList();

            return PartialView("SanPhamHot",list);
        }
        public PartialViewResult DienthoaiHot()
        {

            var list = db.SanPhams.OrderBy(c => c.slTon).Where(c => c.loaiSP == "phone").Take(4);

            return PartialView("DienThoaiHot", list);
        }
        public PartialViewResult LaptopHot()
        {

            var list = db.SanPhams.OrderBy(c => c.slTon).Where(c => c.loaiSP == "laptop").Take(4);
            
            return PartialView("LaptopHot", list);
        }
        public PartialViewResult DongHoHot()
        {

            var list = db.SanPhams.OrderBy(c => c.slTon).Where(c => c.loaiSP == "watch").Take(4);

            return PartialView("DongHoHot", list);
        }

        public ActionResult AppleLaptop()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "apple"&&c.loaiSP=="laptop"
                       select c;
            return View(list);
        }
        public ActionResult DellLaptop()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "dell"
                       select c;
            return View(list);
        }
        public ActionResult HPLaptop()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "hp"
                       select c;
            return View(list);
        }
        public ActionResult AsusLaptop()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "asus"
                       select c;
            return View(list);
        }

        public ActionResult ApplePhone()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "apple"&&c.loaiSP=="phone"
                       select c;
            return View(list);
        }
        public ActionResult SamsungPhone()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "samsung" 
                       select c;
            return View(list);
        }
        public ActionResult XiaomiPhone()
        {

            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "xiaomi" && c.loaiSP == "phone"
                       select c;
            return View(list);
        }
        public ActionResult OppoPhone()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "oppo" 
                       select c;
            return View(list);
        }
        public ActionResult CannonCamera()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "cannon" 
                       select c;
            return View(list);
        }
        public ActionResult SonyCamera()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "sony" 
                       select c;
            return View(list);
        }
        public ActionResult AppleWatch()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "apple" && c.loaiSP == "watch"
                       select c;
            return View(list);
        }
        public ActionResult XiaomiWatch()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "xiaomi" && c.loaiSP == "watch"
                       select c;
            return View(list);
        }
        public ActionResult HuaweiWatch()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "huawei" && c.loaiSP == "watch"
                       select c;
            return View(list);
        }
        public ActionResult LGWatch()
        {
            var list = from c in db.SanPhams
                       where c.Hang.maHang == c.maHang && c.Hang.tenHang == "lg" && c.loaiSP == "watch"
                       select c;
            return View(list);
        }
        public ActionResult BaoHanh()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }

    }
}