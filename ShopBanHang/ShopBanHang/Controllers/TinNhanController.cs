using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class TinNhanController : Controller
    {
        QLShopEntities db = new QLShopEntities();
        // GET: TinNhan
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Mess()
        {
            MessModel messModel = new MessModel();
            string tenTK = "abc"/*@Session["tk"].ToString();*/;
            int id = Int32.Parse((from kh in db.KhachHangs
                                  where kh.taiKhoan == tenTK
                                  select kh.maKH
                                 ).FirstOrDefault().ToString());

            var tn = db.TinNhans.Where(x => x.idGui == id||x.idNhan==id).ToList();
            
            return View(tn);
        }
        public PartialViewResult SendMess()
        {
            

            return PartialView();
        }
        [HttpPost]
        public ActionResult Mess(TinNhan tinNhan)
        {
            string tenTK = "abc"/*@Session["tk"].ToString();*/;
            int id = Int32.Parse((from kh in db.KhachHangs
                                  where kh.taiKhoan == tenTK
                                  select kh.maKH
                                 ).FirstOrDefault().ToString());
            TinNhan tn = new TinNhan();
            tn.idGui = id;
            tn.idNhan = 4;
            tn.Mess = tinNhan.Mess;
            db.TinNhans.Add(tn);
                        
            db.SaveChanges();
            return View();
        }
    }
}