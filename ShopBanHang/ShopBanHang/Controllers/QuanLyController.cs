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
    public class QuanLyController : Controller
    {
        QLShopEntities db = new QLShopEntities();
        // GET: QuanLy
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;
            var list = db.SanPhams.OrderBy(c => c.tenSP).ToPagedList(pageNum, pageSize);
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SanPham sp = db.SanPhams.Where(c => c.maSP == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            var sanpham = db.SanPhams.Where(c => c.maSP == sp.maSP).FirstOrDefault();
            sanpham.tenSP = sp.tenSP;
            sanpham.giaSP = sp.giaSP;
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            SanPham sp = db.SanPhams.Where(c => c.maSP == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Delete(SanPham sp)
        {
            var sanpham = db.SanPhams.Where(c => c.maSP == sp.maSP).FirstOrDefault();
            db.SanPhams.Remove(sanpham);
            return View();

        }
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