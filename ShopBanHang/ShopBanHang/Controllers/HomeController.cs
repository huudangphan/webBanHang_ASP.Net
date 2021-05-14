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

        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();
        public ActionResult Index(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.StartsWith(search)).ToList();
            

            return View(kq);
        }
        public PartialViewResult timKiem(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.Contains(search)).ToList();
            

            return PartialView(kq);
        }
       public ActionResult SanPhamHot()
        {

            //var list = db.SanPhams.OrderBy(c => c.slTon).Take(4).ToList();
            var list = (from sp in db.SanPhams
                        join n in db.CTPNs
                        on sp.maSP equals n.maSP
                        join pn in db.HDNhapSPs
                        on n.maPhieuNhap equals pn.maPhieuNhap
                        orderby pn.ngayNhap descending
                        select sp).Take(4).ToList();
            

            return PartialView("SanPhamHot",list);
        }
        public PartialViewResult DienthoaiHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai ==1 ).Take(4);

            return PartialView("DienThoaiHot", list);
        }
        public PartialViewResult LaptopHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai == 2).Take(4);
            
            return PartialView("LaptopHot", list);
        }
        public PartialViewResult DongHoHot()
        {

            var list = db.SanPhams.Where(c => c.maLoai == 3).Take(4);

            return PartialView("DongHoHot", list);
        }

        public ActionResult AppleLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2&&c.maHang==1
                       select c;
            return View(list);
        }
        public ActionResult DellLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2 &&c.maHang==2
                       select c;
            return View(list);
        }
        public ActionResult HPLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2&&c.maHang==3
                       select c;
            return View(list);
        }
        public ActionResult AsusLaptop()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==2 &&c.maHang==4
                       select c;
            return View(list);
        }

        public ActionResult ApplePhone()
        {
            var list = from c in db.SanPhams
                       where c.maHang==1&&c.maLoai==1
                       select c;
            return View(list);
        }
        public ActionResult SamsungPhone()
        {
            var list = from c in db.SanPhams
                       where c.maLoai==1&&c.maHang==5 
                       select c;
            return View(list);
        }
        public ActionResult XiaomiPhone()
        {

            var list = from c in db.SanPhams
                       where c.maLoai==1&&c.maHang==6
                       select c;
            return View(list);
        }
        public ActionResult OppoPhone()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 1 && c.maHang == 7
                       select c;
            return View(list);
        }
        public ActionResult CannonCamera()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 4 && c.maHang == 8
                       select c;
            return View(list);
        }
        public ActionResult SonyCamera()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 4 && c.maHang == 9
                       select c;
            return View(list);
        }
        public ActionResult AppleWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 1
                       select c;
            return View(list);
        }
        public ActionResult XiaomiWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 6
                       select c;
            return View(list);
        }
        public ActionResult HuaweiWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 10
                       select c;
            return View(list);
        }
        public ActionResult LGWatch()
        {
            var list = from c in db.SanPhams
                       where c.maLoai == 3 && c.maHang == 11
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
        public ActionResult TimKiemKQ(string tukhoa)
        {
            
            var kq = db.SanPhams.Where(x=>x.tenSP.Contains(tukhoa)).ToList();
            return View(kq);
        }

    }
}