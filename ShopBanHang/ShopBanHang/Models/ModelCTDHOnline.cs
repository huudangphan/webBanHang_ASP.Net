using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class ModelCTDHOnline
    {
        public static int mahd { get; set; }
        
       
        public  int sl { get; set; }
        public  double donGia { get; set; }
        public  double thanhTien { get { return sl * donGia; } }
        public ModelCTDHOnline(int sl,double donGia)
        {
            this.sl = sl;
            this.donGia = donGia;
        }


        
       

    }
}