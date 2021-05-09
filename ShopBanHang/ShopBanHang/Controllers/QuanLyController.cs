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
        public ActionResult Index2(int? page)
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
        #region quan ly hoa don online
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
        public ViewResult QLDonHangOnline()
        {
            var result = db.HDOnlines.ToList();
            return View(result);
        }
        public ActionResult XemChiTietDonHang(int mahd)
        {
            ModelCTDHOnline.mahd = mahd;                  

            return View();
        }
       
       public PartialViewResult viewKhach()
        {
            var listkh = (from kh in db.KhachHangs
                          join hd in db.HDOnlines
                          on kh.MaKH equals hd.MaKH
                          where hd.MaHD == ModelCTDHOnline.mahd
            select kh
                        ).ToList();
            return PartialView(listkh);
        }
        public PartialViewResult viewSP()
        {
            var listsp = (from ct in db.CTHDOnlines
                          join sp in db.SanPhams
                          on ct.MaSP equals sp.maSP
                          where ct.MaHD == ModelCTDHOnline.mahd
                          select sp
                       ).ToList();
            return PartialView(listsp);
        }
        public PartialViewResult viewHDOnline()
        {
            var listDH = db.HDOnlines.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();
            return PartialView(listDH);
        }
        public PartialViewResult viewCTHDOnline()
        {
            var listcDH = db.CTHDOnlines.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();
            
           
            return PartialView(listcDH);
        }

        public ActionResult GuiHang(int madh, FormCollection f)
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
                    try
                    {

                        var s = (from k in db.CTTonKhoes
                                 where k.MaKho == maKho && k.MaSP == itemct.maSP
                                 select k).ToList();
                        //list san pham ton kho
                        foreach (var Tonkho in s)
                        {
                            if (item.SL < Tonkho.SL)
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

        #endregion
        #region quan ly hoa don offline

        public PartialViewResult viewKhachOff()
        {
            var listkhOff = (from kh in db.KhachHangs
                          join hd in db.HDOffLines
                          on kh.MaKH equals hd.MaKH
                          where hd.MaHD == ModelCTDHOnline.mahd
                          select kh
                        ).ToList();
            return PartialView(listkhOff);
        }
        public PartialViewResult viewSPOff()
        {
            var listspOff = (from ct in db.CTHDOffs
                          join sp in db.SanPhams
                          on ct.MaSP equals sp.maSP
                          where ct.MaHD == ModelCTDHOnline.mahd
                          select sp
                       ).ToList();
            return PartialView(listspOff);
        }
        public PartialViewResult viewHDOffline()
        {
            var listDHOff = db.HDOffLines.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();
            return PartialView(listDHOff);
        }
        public PartialViewResult viewCTHDOffline()
        {
            var listcDH = db.CTHDOffs.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();


            return PartialView(listcDH);
        }
        public ViewResult QlDonHangOffline()
        {
            return View(db.HDOffLines.ToList());
        }
        public ActionResult XemCTHDOff(int mahd)
        {
            ModelCTDHOnline.mahd = mahd;                    
            

            return View();

        }
        #endregion
        #region quan ly hoa don tra gop
        public ViewResult QLHDTG()
        {
            return View(db.HDTraGops.ToList());
        }
        public PartialViewResult viewKhachTG()
        {
            var listkhOff = (from kh in db.KhachHangs
                             join hd in db.HDTraGops
                             on kh.MaKH equals hd.MaKH
                             where hd.MaHD == ModelCTDHOnline.mahd
                             select kh
                        ).ToList();
            return PartialView(listkhOff);
        }
        public PartialViewResult viewSPTG()
        {
            var listspOff = (from ct in db.CTHDTGs
                             join sp in db.SanPhams
                             on ct.MaSP equals sp.maSP
                             where ct.MaHD == ModelCTDHOnline.mahd
                             select sp
                       ).ToList();
            return PartialView(listspOff);
        }
        public PartialViewResult viewHDTG()
        {
            var listDHOff = db.HDTraGops.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();
            return PartialView(listDHOff);
        }
        public PartialViewResult viewCTHDTG()
        {
            var listcDH = db.CTHDTGs.Where(x => x.MaHD == ModelCTDHOnline.mahd).ToList();


            return PartialView(listcDH);
        }
        public ViewResult XemCTHDTG(int mahd)
        {
            ModelCTDHOnline.mahd = mahd;

            return View();
        }
        #endregion
       
           // tim thong tin khach hang neu khach da tung mua hang o cua hang
           // nếu không thì tạo thông tin khách hàng trước khi mua
           // chọn sản phẩm vào giỏ hàng trước rồi mới chọn hình thức thanh toán
        public ActionResult TaoHDTG(string tuKhoa)
        {
            var result = db.KhachHangs.Where(x => x.tenKH.Contains(tuKhoa)).ToList();
            return View(result);
        }   
        public ActionResult TaoHDOff(string tuKhoa)
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
            KhachHang khach = new KhachHang();
            khach.tenKH = kh.tenKH;
            khach.DiaChi = kh.diaChi;
            khach.SDT = kh.sdt;
            khach.email = kh.email;
            khach.tongMua = 0;
            db.KhachHangs.Add(khach);
            db.SaveChanges();
            return RedirectToAction("TaoHDTG", "QuanLy");
        }
        public PartialViewResult TimKiemKhachHang(string tuKhoa)
        {
                        
            var  result = db.KhachHangs.Where(x => x.tenKH.Contains(tuKhoa)).ToList();         
                       
            return PartialView(result);
        }
    
        public ActionResult TaoPhieuTraGop()
        {
            int mahd = ModelCTDHOnline.mahd;
            return View(db.PhieuTraGops.Where(x=>x.MaHD==mahd).ToList());

        }
       
        public ActionResult TaoPhieuTraGop2(int maPhieu)
        {

            int mahd = ModelCTDHOnline.mahd;
            PhieuTraGop ptg = db.PhieuTraGops.Where(x => x.MaPhieu == maPhieu).FirstOrDefault();
            ptg.NgayTra = DateTime.Parse(DateTime.Now.ToString()); /*DateTime.Parse("2022-10-10");*/
            db.SaveChanges();
            return View(db.PhieuTraGops.Where(x=>x.MaPhieu==maPhieu).SingleOrDefault());

        }


    }
}