using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using PagedList;
using PagedList.Mvc;

namespace ShopBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        QLShopEntities db = new QLShopEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult TimKiemKQ(FormCollection f,int? page)
        {
            string tuKhoa = f["txtTimKiem"].ToString();
            List<SanPham> kqtk = db.SanPhams.Where(n => n.tenSP.Contains(tuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if(kqtk.Count==0)
            {
                ViewBag.tb = "Không tìm thấy sản phẩm nào phù hợp";
                return RedirectToAction("Index", "Home");
            }    
            return View(kqtk.OrderBy(n=>n.tenSP).ToPagedList(pageNumber,pageSize));
        }
    }
}