using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
   
    public class SanPhamController : Controller
    {
        databaseEntities1 db = new databaseEntities1();

        //databaseEntities db = new databaseEntities();
        // GET: SanPham
        public ViewResult xemChiTiet(int masp = 0)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
    }
}