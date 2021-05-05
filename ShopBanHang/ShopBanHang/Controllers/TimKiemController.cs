using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopBanHang.Models;
namespace ShopBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        ShopDoCongNgheEntities db = new ShopDoCongNgheEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TimKiemKQ(string search)
        {
            var kq = db.SanPhams.Where(x => x.tenSP.StartsWith(search)).ToList();


            return View(kq);
        }
    }
}