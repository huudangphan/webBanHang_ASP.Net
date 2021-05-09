using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class AdminController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string username = f["username"].ToString();
            string password = f["password"].ToString();
            if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                ViewBag.Loi1 = "Tên đăng nhập và mật khẩu không được để trống";
            }
            else
            {
                Admin ad = db.Admins.Where(x => x.userAdmin == username && x.passAdmin == password).SingleOrDefault();
                if (ad != null)
                {
                    if (username == "Admin")
                        return RedirectToAction("IndexQuanLy", "QuanLy");
                    return RedirectToAction("Index", "QuanLy");
                }
                else
                    ViewBag.Loi2 = "Tên đăng nhập hoặc mật khẩu sai";
            }
            
            

            

            return View();

        }
    }
}