using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        SanPhamQuery sp = new SanPhamQuery();
        [HttpGet]
        public string getAllSanPham()
        {
            return sp.getAllSP();

        }
        [HttpGet]
        public string getSP(string tensp)
        {
            return sp.getSP(tensp);
        }
        [HttpGet]
        public string GetHang()
        {
            return sp.getHang();
        }
        [HttpGet]
        public string GetLoai()
        {
            return sp.GetLoaiSP();
        }
        [HttpPost]
        public void ThemSP(string maHang,string maLoai,string tenSP,string anh,string giaSP,string MoTa)
        {
            sp.ThemSP(maHang, maLoai, tenSP, anh, giaSP, MoTa);
        }
        [HttpGet]
        public string GetChiTietKho(string makho)
        {
            return sp.ChiTietKho(makho);
        }
        [HttpPost]
        public void themCTTonKho(int makho,int sl)
        {
            sp.ThemKho(makho, sl);
        }
        [HttpGet]
        public string XemCTKho(int makho)
        {
            return sp.xemCTkho(makho);
        }
    }
}
