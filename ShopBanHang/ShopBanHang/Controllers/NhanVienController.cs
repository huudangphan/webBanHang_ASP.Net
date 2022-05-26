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
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();
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
            {
                ViewBag.result = "-1";
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                
                var result = db.KhachHangs.Where(x => x.sodt == search).FirstOrDefault();
                if (result != null)
                {
                    ViewBag.result = "1";
                    return PartialView(result);
                }
                else
                {
                    ViewBag.result = "-1";
                    Response.StatusCode = 404;
                    return null;
                }

          
            }

          
        }

        


    }
}