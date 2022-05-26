using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class user
    {
        public string curpass { get; set; }
        public string pass { get; set; }
        public string pass2 { get; set; }
        public static int id { get; set; }
        public static double tongtien { get; set; }
    }
}