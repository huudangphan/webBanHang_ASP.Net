using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

using ShopBanHang.MultiData;


namespace ShopBanHang.Controllers
{
    public class QuanLyController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;
            var list = db.SanPhams.OrderBy(c => c.tenSP).ToPagedList(pageNum, pageSize);
            return View(list);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.maHang = new SelectList(db.Hangs.ToList(), "maHang", "tenHang");

            return View();
        }
        public PartialViewResult timKiem(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.Contains(search)).ToList();


            return PartialView(kq);
        }
        [HttpPost]
        public ActionResult Add(SanPham sp, HttpPostedFileBase fileUp)
        {
            if (ModelState.IsValid)
            {
                // Luu ten file
                var fileName = Path.GetFileName(fileUp.FileName);
                // Luu duong dan
                var path = Path.Combine(Server.MapPath("~/Assit/img"), fileName);
                // kiem tra hinh anh da ton tai chua
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else { fileUp.SaveAs(path); }
                SanPham a = new SanPham() { anh = fileUp.FileName };
                sp.anh = fileUp.FileName;
                db.SanPhams.Add(sp);
                //db.SanPhams.Add(sp);

                db.SaveChanges();
            }

            ViewBag.maHang = new SelectList(db.Hangs.ToList(), "maHang", "tenHang");
            return RedirectToAction("Index", "QuanLy");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SanPham sp = db.SanPhams.Where(c => c.maSP == id).SingleOrDefault();
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            var sanpham = db.SanPhams.Where(c => c.maSP == sp.maSP).FirstOrDefault();
            sanpham.tenSP = sp.tenSP;
            sanpham.giaSP = sp.giaSP;
            sanpham.MoTa = sp.MoTa;
            db.SaveChanges();
            ViewBag.TB = "Chỉnh sửa thành công";
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            SanPham sp = db.SanPhams.Where(c => c.maSP == id).SingleOrDefault();
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var sanpham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLy");

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
        #region code cu
        //public ViewResult DetailsAcc(int makh)
        //{
        //    var kh = db.KhachHangs.Where(x => x.maKH == makh);
        //    return View(kh.ToList());
        //}
        //public ViewResult DetailDonHang(int makh)
        //{
        //    var detail = (from kh in db.KhachHangs
        //                  join dh in db.DonHangs
        //                  on kh.maKH equals dh.maKH
        //                  join ctdh in db.ChiTietDonHangs
        //                  on dh.maDH equals ctdh.maDH
        //                  where kh.maKH == makh
        //                  select dh).ToList();
        //    return View(detail);
        //}
        //[HttpGet]


        //public ActionResult QLDonHang()
        //{
        //    var mymodel = new MultiDataa();
        //    mymodel.donhang = db.DonHangs.ToList();

        //    return View(mymodel);
        //}

        //public ActionResult DonHangChuaGiao()
        //{

        //    var mymodel = new MultiDataa();
        //    mymodel.donhang = db.DonHangs.Where(x => x.tinhTrang == 0).ToList();
        //    return View(mymodel);
        //}


        //public ActionResult DonHangDaGiao()
        //{
        //    var mymodel = new MultiDataa();
        //    mymodel.donhang = db.DonHangs.Where(x => x.tinhTrang == 1).ToList();
        //    return View(mymodel);
        //}
        public ActionResult XemChiTietDonHang(int madh)
        {

            var mymodel = new MultiDataa();
            mymodel.donhang = db.HDOnlines.Where(x => x.MaHD== madh).ToList();
            mymodel.ctdonhang = db.CTHDOnlines.Where(x => x.MaHD == madh).ToList();
            var makh = (from c in mymodel.donhang
                        where c.MaHD == madh
                        select c.MaKH).FirstOrDefault();

            mymodel.khachhang = db.KhachHangs.Where(c => c.MaKH == makh);
            mymodel.sanPhams = (from a in db.CTHDOnlines
                                join b in db.SanPhams
                                on a.MaSP equals b.maSP
                                where a.MaHD == madh
                                select b).ToList();


            return View(mymodel);
        }
        //public ActionResult GuiHang(int madh)
        //{
        //    var hang = db.DonHangs.SingleOrDefault(c => c.maDH == madh);

        //    var listsp = (from c in db.ChiTietDonHangs
        //                  join dh in db.DonHangs
        //                  on c.maDH equals dh.maDH
        //                  where c.maDH == madh
        //                  select c).ToList();
        //    var listct = (from ct in db.ChiTietDonHangs
        //                  join d in db.DonHangs
        //                  on ct.maDH equals d.maDH
        //                  join sp in db.SanPhams
        //                  on ct.maSP equals sp.maSP
        //                  where ct.maDH == madh && ct.maSP == sp.maSP
        //                  select sp).ToList();
        //    foreach (var item in listsp)
        //    {
        //        foreach (var itemct in listct)
        //        {
        //            if (item.soLuong > itemct.slTon)
        //            {
        //                hang.tinhTrang = 1;
        //                hang.daThanhToan = 1;
        //                DateTime date = DateTime.Now;
        //                hang.ngayGiao = date;
        //                itemct.slTon -= 1;
        //            }
        //            else
        //            {
        //                ViewBag.error = "Số lượng hàng tồn trong kho không đủ";
        //                return View();
        //            }

        //        }
        //    }


        //    db.SaveChanges();
        //    return RedirectToAction("Index", "QuanLy");
        //}
        //public ActionResult QLTaiKhoan()
        //{
        //    var acc = db.KhachHangs.ToList();


        //    return View(acc);
        //}
        //public ActionResult ListTn()
        //{
        //    var list = (from kh in db.KhachHangs
        //                join tn in db.TinNhans
        //                on kh.maKH equals tn.idGui
        //                where kh.maKH == tn.idGui
        //                select kh).Distinct().ToList();
        //    return View(list);

        //}
        //[HttpPost]
        //public ActionResult Mess(string tentk = "")
        //{
        //    var mess = (from tn in db.TinNhans
        //                join kh in db.KhachHangs
        //                on tn.idGui equals kh.maKH
        //                where kh.taiKhoan == tentk
        //                select tn).ToList();
        //    return View(mess);
        //}
        #endregion
        public ViewResult QLDonHangOnline()
        {
            var result = db.HDOnlines.ToList();
            return View(result);
        }
        public ViewResult QlDonHangOffline()
        {
            return View(db.HDOffLines.ToList());
        }
        public ActionResult XemCTHDOff(int mahd)
        {
            var mymodel = new XCTDHOffline();
            mymodel.hdOffline = db.HDOffLines.Where(x => x.MaHD==mahd).ToList();
            mymodel.cthdOffline = db.CTHDOffs.Where(x => x.MaHD==mahd).ToList();
            var makh = (from c in mymodel.hdOffline
                        where c.MaHD == mahd
                        select c.MaKH).FirstOrDefault();

            mymodel.khachang = db.KhachHangs.Where(c => c.MaKH == makh);
            mymodel.sapham = (from a in db.CTHDOffs
                                join b in db.SanPhams
                                on a.MaSP equals b.maSP
                                where a.MaHD == mahd
                                select b).ToList();

            return View(mymodel);

        }
        public ViewResult QLHDTG()
        {
            return View(db.HDTraGops.ToList());
        }
        public ViewResult CTHDTG(int mahd)
        {
            var mymodel = new XCTHDTG();
            mymodel.hdtragop = db.HDTraGops.Where(x => x.MaHD == mahd).ToList();
            mymodel.cthdtg = db.CTHDTGs.Where(x => x.MaHD == mahd).ToList();
            var makh = (from c in mymodel.hdtragop
                        where c.MaHD == mahd
                        select c.MaKH).FirstOrDefault();

            mymodel.khachang = db.KhachHangs.Where(c => c.MaKH == makh);
            mymodel.sapham = (from a in db.CTHDTGs
                              join b in db.SanPhams
                              on a.MaSP equals b.maSP
                              where a.MaHD == mahd
                              select b).ToList();


            return View(mymodel);
        }
        public ActionResult GuiHang(int madh,FormCollection f)
        {
            int maKho = 1 /*Int32.Parse(f["txtMaKho"]);*/;
            var hang = db.HDOnlines.SingleOrDefault(c => c.MaHD == madh);
            // lay hoa don online
            var listsp = (from c in db.CTHDOnlines
                          join dh in db.HDOnlines
                          on c.MaHD equals dh.MaHD
                          where c.MaHD == madh
                          select c).ToList();
            // lay chi tiet hoa don online
            var listct = (from ct in db.CTHDOnlines
                          join d in db.HDOnlines
                          on ct.MaHD equals d.MaHD
                          join sp in db.SanPhams
                          on ct.MaSP equals sp.maSP
                          where ct.MaHD == madh && ct.MaSP == sp.maSP
                          select sp).ToList();
            // lay list san pham trong chi tiet hoa don
            foreach (var item in listsp)
            {
                foreach (var itemct in listct)
                {
                    try                    {

                        var s = (from k in db.CTTonKhoes
                                 where k.MaKho == maKho && k.MaSP == itemct.maSP
                                 select k).ToList();
                        //list san pham ton kho
                        foreach (var Tonkho in s)
                        {
                            if (item.SL <Tonkho.SL)
                            // kiem tra so luong hang trong kho > so luong hang ban???
                            {
                                hang.TinhTrang = true;

                                DateTime date = DateTime.Now;
                                hang.NgayGiao = date;
                                var kho = db.CTTonKhoes.Where(x => x.MaKho == maKho && x.MaSP == itemct.maSP).SingleOrDefault();
                                kho.SL -= item.SL;
                                hang.TinhTrang = true;
                                // giam so luong hang ton kho
                            }
                            else
                            {
                                ViewBag.error = "Số lượng hàng tồn trong kho không đủ";
                                return View();
                            }
                        }

                        
                    }
                    catch (Exception ex)
                    {

                        ViewBag.error = ex.ToString();
                    }                   
                   
                }
            }


            db.SaveChanges();
            return RedirectToAction("Index", "QuanLy");
        }
                
        public ActionResult TaoHDTG(string tuKhoa)
        {
            var result = db.KhachHangs.Where(x => x.tenKH.Contains(tuKhoa)).ToList();
            return View(result);
        }   
       
        [HttpGet]
        public ActionResult TaoKhachHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoKhachHang(ThongTinKhachHang kh)
        {
            return View();
        }
        public PartialViewResult TimKiemKhachHang(string tuKhoa)
        {
                        
            var  result = db.KhachHangs.Where(x => x.tenKH.Contains(tuKhoa)).ToList();         
                       
            return PartialView(result);
        }
    }
}