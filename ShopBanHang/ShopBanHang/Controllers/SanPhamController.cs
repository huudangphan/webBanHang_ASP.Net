using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{

    public class SanPhamController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

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
        public ViewResult XemSP(int masp = 0)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.maSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var kho = db.CTTonKhoes.Where(x => x.MaSP == masp && x.MaKho == 1).FirstOrDefault();
            if(kho!=null)
                ViewBag.sl = kho.SL;
            else
                ViewBag.sl = 0;


            return View(sp);
        }
        [HttpGet]
        public ActionResult FileUpload()
        {
            ViewBag.maloai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoaiSP), "maLoai", "TenLoaiSP");
            ViewBag.mahang = new SelectList(db.Hangs.ToList().OrderBy(n => n.tenHang), "maHang", "tenHang");
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(SanPham sp, HttpPostedFileBase fileUp)
        {

            if (ModelState.IsValid)
            {
                // Luu ten file
                var fileName = Path.GetFileName(fileUp.FileName);
                // Luu duong dan
                var path = Path.Combine(Server.MapPath("~/Assit/img"), fileName);
                // kiem tra hinh anh da ton tai chua
                ViewBag.maloai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoaiSP), "maLoai", "TenLoaiSP");
                ViewBag.mahang = new SelectList(db.Hangs.ToList().OrderBy(n => n.tenHang), "maHang", "tenHang");
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else { 
                    fileUp.SaveAs(path);
                  
                    SanPham sanpham = new SanPham() { anh = fileUp.FileName };
                    sp.anh = fileUp.FileName;
                    db.SanPhams.Add(sp);

                    db.SaveChanges();
                }
                

            }
            return View();
        }
        [HttpGet]
        public ActionResult SuaSanPham(int masp)
        {
            return View(db.SanPhams.Where(x=>x.maSP==masp).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult SuaSanPham(SanPham sp)
        {
            try
            {
                var sanpham = db.SanPhams.Where(x => x.maSP == sp.maSP).FirstOrDefault();
                sanpham.tenSP = sp.tenSP;
                sanpham.giaSP = sp.giaSP;
                sanpham.MoTa = sp.MoTa;
                db.SaveChanges();
                ViewBag.tb = "Chỉnh sửa thông tin sản phẩm thành công";
                return View();
            }
            catch
            {
                ViewBag.tb = "Đã xảy ra lỗi, vui lòng thử lại sau";
            }

            return View();
        }

    }
}