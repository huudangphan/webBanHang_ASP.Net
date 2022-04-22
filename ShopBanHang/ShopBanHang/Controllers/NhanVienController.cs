using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
namespace ShopBanHang.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        ShopDoCongNgheEntities1 db = new ShopDoCongNgheEntities1();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SanPham()
        {
            return View();
        }
        public PartialViewResult timKiem(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.Contains(search)).ToList();


            return PartialView(kq);
        }
        public ActionResult KhachHang()
        {
            return View();
        }
        public PartialViewResult TimKiemKhach(string search)
        {
            if (string.IsNullOrEmpty(search))
                return PartialView(db.KhachHangs.Where(x=>x.MaKH==45).FirstOrDefault());
            var result = db.KhachHangs.Where(x => x.sodt == search).FirstOrDefault();
            return PartialView(result);
        }




    }
}