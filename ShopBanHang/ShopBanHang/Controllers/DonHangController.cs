using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
namespace ShopBanHang.Controllers
{
    public class DonHangController : Controller
    {
        ShopDoCongNgheEntities1 db = new ShopDoCongNgheEntities1();
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DonHangOnline()
        {
            try
            {
                return View(db.HDOnlines.Where(x=>x.MaHD==160).ToList());
            }
            catch (Exception ex)
            {

                return View();
            }
            
        }
        [HttpPost]
        public ActionResult DonHangOnline(FormCollection f)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(f["fromDate"]);
                DateTime toDate = DateTime.Parse(f["toDate"]);
                var result = db.HDOnlines.Where(x => x.NgayDat >= fromDate && x.NgayDat <= toDate).ToList();
                return View(result);
            }
            catch (Exception)
            {

                return View();
            }
           
        }
        public ActionResult ChiTietDonHangOnline(int id)
        {
            var result = db.CTHDOnlines.Where(x => x.MaHD == id).ToList();
            GioHang.makh = id;
            return View(result);
        }
        public PartialViewResult KhachHang()
        {
            var result = db.KhachHangs.Where(x => x.MaKH == GioHang.makh).FirstOrDefault();
            return PartialView(result);
        }

    }
}