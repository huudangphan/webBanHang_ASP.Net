using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        KhachHangQuery kh = new KhachHangQuery();
        [HttpPost]
        public void ThemKhachHang(string tenkh,string diachi,int sdt,string email,string taikhoan,string matkhau)
        {
            kh.ThemKhachHang(tenkh,diachi,sdt,email,taikhoan,matkhau);
        }
        //[HttpGet("{Ten}")]
        //public string TimKhachHang(string tenkh)
        //{
        //    return kh.TimKhachHang(tenkh);
        //}
        [HttpGet("{sdt}")]
        public string TimKHSDT(string sdt)
        {
            return kh.TimKhachHang(sdt);
        }
    }
}
