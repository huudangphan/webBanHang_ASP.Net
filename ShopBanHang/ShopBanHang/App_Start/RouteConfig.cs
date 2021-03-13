using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "appleLaptop",
               url: "appleLaptop",
               defaults: new { controller = "Home", action = "AppleLaptop", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "appleWatch",
               url: "appleWatch",
               defaults: new { controller = "Home", action = "ApplePhone", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "asusLaptop",
               url: "asusLaptop",
               defaults: new { controller = "Home", action = "AsusLaptop", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "cannonCamera",
               url: "cannonCamera",
               defaults: new { controller = "Home", action = "CannonCamera", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "dellLaptop",
               url: "dellLaptop",
               defaults: new { controller = "Home", action = "DellLaptop", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "hpLaptop",
               url: "HPLaptop",
               defaults: new { controller = "Home", action = "HPLaptop", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "huaweiWatch",
               url: "huaweiWatch",
               defaults: new { controller = "Home", action = "HuaweiWatch", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "lgWatch",
               url: "lgWatch",
               defaults: new { controller = "Home", action = "LGWatch", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "oppoPhone",
               url: "oppoPhone",
               defaults: new { controller = "Home", action = "OppoPhone", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "samsungPhone",
               url: "samsungPhone",
               defaults: new { controller = "Home", action = "SamsungPhone", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "sonyCamera",
               url: "sonyCamera",
               defaults: new { controller = "Home", action = "SonyCamera", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "xiaomiPhone",
               url: "xiaomiPhone",
               defaults: new { controller = "Home", action = "XiaomiPhone", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "xiaomiWatch",
               url: "xiaomiWatch",
               defaults: new { controller = "Home", action = "XiaomiWatch", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "baoHanh",
               url: "baoHanh",
               defaults: new { controller = "Home", action = "BaoHanh", id = UrlParameter.Optional }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "gioiThieu",
               url: "gioiThieu",
               defaults: new { controller = "Home", action = "GioiThieu", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "index",
               url: "index",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
            name: "dangnhap",
            url: "dangnhap",
            defaults: new { controller = "NguoiDung", action = "DangNhap", id = UrlParameter.Optional }
        );
            routes.MapRoute(
           name: "dangKy",
           url: "dangKy",
           defaults: new { controller = "NguoiDung", action = "DangKy", id = UrlParameter.Optional }
       );
            routes.MapRoute(
         name: "hoso",
         url: "hoso",
         defaults: new { controller = "NguoiDung", action = "HoSoNguoiDung", id = UrlParameter.Optional }
     );








            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "applePhone",
               url: "applePhone",
               defaults: new { controller = "Home", action = "ApplePhone", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
