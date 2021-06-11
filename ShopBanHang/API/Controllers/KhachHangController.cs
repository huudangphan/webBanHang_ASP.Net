using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        KhachHangQuery kh = new KhachHangQuery();
        [HttpPost]
        public void ThemKhachHang(string tenkh,string diachi,int sdt,string email,string taikhoan,string matkhau)
        {
            kh.ThemKhachHang(tenkh,diachi,sdt,email,taikhoan,matkhau);
        }
        [HttpPost]
        public void ThemKhachHangkUser(string tenkh, string diachi, int sdt, string email)
        {
            kh.ThemKhachHangkUser(tenkh, diachi, sdt, email);
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
        [HttpGet]
        public string getAll()
        {
            return kh.getAll();
        }
        [HttpGet]
        public  string GetKhachHang(string sodt)
        {
            return kh.GetKhachHang(sodt);
        }
    }
}
