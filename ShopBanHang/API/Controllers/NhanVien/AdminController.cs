using API.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.NhanVien
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        AdminQuery nv = new AdminQuery();
        [HttpGet]
        public string getNhanVien()
        {
            return nv.GetNhanVien();
        }
        [HttpPost]
        public void InsertNhanVien(string username,string password,string tennv,int loai)
        {
            nv.InsertNhanVien(username, password, tennv, loai);
        }
        [HttpPut]
        public void UpdateNhanVien(string username,string password,string ten,int loai)
        {
            nv.UpdateNhanVien(username, password,ten,loai);  
        }
        [HttpGet]
        public string DoanhThuOnline(string thang,string nam)
        {
            return nv.DoanhThuOnline(thang, nam);
        }
        [HttpGet]
        public string DoanhThuOffline(string thang,string nam)
        {
            return nv.DoanhThuOffline(thang,nam);
        }
        [HttpGet]
        public string DoanhThuTG(string nam,string thang)
        {
            return nv.DoanhThuTG(thang,nam);
        }
        [HttpGet]
        public string DoanhThuTGThang(string nam, string thang)
        {
            return nv.DoanhThuTGHangThang(thang, nam);
        }
        [HttpGet]
        public string DoanhThuPhat(string nam,string thang)
        {
            return nv.DoanhThuPhat(thang,nam);
        }
        [HttpGet]
        public string TongChi(string nam,string thang)
        {
            return nv.TongChi(thang,nam);
        }
        [HttpGet]
        public string GetPass(string password)
        {
            return nv.GetPass(password);
        }
        [HttpPut]
        public void DoiMatKhau(string password)
        {
            nv.DoiMatKhau(password);
        }
    }
}
