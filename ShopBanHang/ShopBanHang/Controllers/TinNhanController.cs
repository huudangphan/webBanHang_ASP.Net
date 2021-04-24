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
        //QLShopEntities db = new QLShopEntities();
        //// GET: TinNhan
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult Mess()
        //{
        //    try
        //    {
        //        MessModel messModel = new MessModel();
        //        string tenTK = "abc"/*@Session["tk"].ToString();*/;
        //        int id = Int32.Parse((from kh in db.KhachHangs
        //                              where kh.taiKhoan == tenTK
        //                              select kh.maKH
        //                             ).FirstOrDefault().ToString());

        //        var tn = db.TinNhans.Where(x => x.idGui == id || x.idNhan == id).ToList();

        //        return View(tn);
        //    }
        //    catch (Exception)
        //    {

        //        return View();
        //    }           
        //}       
        //[HttpPost]
        //public ActionResult Mess(FormCollection f)
        //{
        //    string tenTK = "abc"/*@Session["tk"].ToString();*/;
        //    int id = Int32.Parse((from kh in db.KhachHangs
        //                          where kh.taiKhoan == tenTK
        //                          select kh.maKH
        //                         ).FirstOrDefault().ToString());
        //    string tin = f["TinNhan"];
        //    TinNhan tn = new TinNhan();
        //    tn.Mess = tin;
        //    tn.idNhan = 4;
        //    tn.idGui = 5;
        //    tn.time = DateTime.Now;
        //    db.TinNhans.Add(tn);
                        
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}