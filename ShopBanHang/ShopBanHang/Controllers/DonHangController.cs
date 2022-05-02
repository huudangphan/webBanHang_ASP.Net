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
            var result = db.CTHDOnlines.Where(x => x.MaHD == id).FirstOrDefault();
            var temp = db.HDOnlines.Where(x => x.MaHD == id).FirstOrDefault();
            GioHang.makh = Int32.Parse(temp.MaKH.ToString());
            return View(result);
        }
        public PartialViewResult KhachHang()
        {
            var result = db.KhachHangs.Where(x => x.MaKH == GioHang.makh).FirstOrDefault();
            return PartialView(result);
        }
        public PartialViewResult ChiTietSanPham(int id)
        {
            return PartialView(db.SanPhams.Where(x => x.maSP == id).FirstOrDefault());
        }
        public ActionResult GuiHang(int id)
        {
            var dh = db.HDOnlines.Where(x => x.MaHD == id).FirstOrDefault();
            if (dh.TinhTrang == true)
            {
                ViewBag.result = "Đơn hàng đã được gửi rồi";
                return View();
            }

                //return JavaScript("alert('Đơn hàng đã được gửi rồi')");
            else
            {
                if(!CheckGiaoHang(id))
                {
                    ViewBag.result = "Hàng tồn không đủ";
                    return View();
                }
                    //return JavaScript("alert('Hàng tồn không đủ')");
                else
                {
                    dh.TinhTrang = true;
                    db.SaveChanges();
                }    
            }
            ViewBag.result = "Xác nhận đơn hàng thành công";
            //return JavaScript("alert('Xác nhận đơn hàng thành công')");
            return View();
        }
        private bool CheckGiaoHang(int id)
        {
            var ctdh = db.CTHDOnlines.Where(x => x.MaHD == id).ToList();
            foreach (var item in ctdh)
            {
                var sl = db.CTTonKhoes.Where(x => x.MaSP == item.MaSP && x.MaKho == item.MaKho).SingleOrDefault();
                if (sl.SL < item.SL)
                    return false;

            }
            return true;
        }
        public PartialViewResult KhachMuaHang()
        {
            return PartialView(db.KhachHangs.Where(x=>x.MaKH==user.id).SingleOrDefault());
        }
        [HttpGet]
        public ActionResult TaoDonHangOffline()
        {
            if (string.IsNullOrEmpty(user.id.ToString()) || Session["GioHang"] == null)
            {
                ViewBag.result = "Vui lòng chọn khách hàng và sản phẩm";
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult TaoDonHangOffline(FormCollection f)
        {
            try
            {
                
                HDOffLine hd = new HDOffLine();
                hd.MaKH = user.id;
                hd.NgayMua = DateTime.Now;
                db.HDOffLines.Add(hd);              
                List<GioHang> gioHang = getGioHang();
                foreach (var item in gioHang)
                {
                    CTHDOff ct = new CTHDOff();
                    ct.MaHD = hd.MaHD;
                    ct.MaSP = item.maSP;
                    ct.SL = item.soLuong;
                    ct.MaKho = 3;
                    ct.MaSP = item.maSP;
                    ct.GiaBan = item.donGia;
                    db.CTHDOffs.Add(ct);
                }
                db.SaveChanges();
                user.id = -1;
                Session["GioHang"] = null;
                ViewBag.result = "Mua hàng thành công";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.result = ex.Message.ToString();
                return View();
            }                    
                      
        }
        
        public List<GioHang> getGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }// nếu chưa có giỏ hàng
            return listGioHang;
        }
        [HttpGet]
        public ActionResult TaoDonHangTraGop()
        {

            if (string.IsNullOrEmpty(user.id.ToString()) || Session["GioHang"] == null)
            {
                ViewBag.result = "Vui lòng chọn khách hàng và sản phẩm";
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult TaoDonHangTraGop(FormCollection f)
        {
            return View();

        }
    }
}