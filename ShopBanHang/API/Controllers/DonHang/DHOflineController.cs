using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query.DonHangQuery;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.DonHang
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DHOflineController : ControllerBase
    {
        DHOfflineQuery dh = new DHOfflineQuery();
        [HttpPost]
        public void TaoHDOffline(int makh)
        {
            dh.TaoDHOffline(makh);
        }
        [HttpPost]
        public void TaoCTHDOff(int masp,double giaBan,int SL,int makho)
        {
            dh.TaoCTDOff(makho,masp, SL, giaBan);
        }
        [HttpGet]
        public string HDOff(int makh)
        {
            return dh.HDOff(makh);
        }
        [HttpGet]
        public string getHDOff()
        {
            return dh.getHDOffline();
        }
        [HttpGet]
        public string getlastdh()
        {
            return dh.GetLastDH();
        }
        [HttpGet]
        public string getCTHDOff(int mahd)
        {
            return dh.getCTHDOff(mahd);
        }
        [HttpGet]
        public string GetMaHD()
        {
            return dh.getmahdmax();
        }
        [HttpGet]
        public string GetMaCTHD()
        {
            return dh.getmacthdmax();
        }
        [HttpDelete]
        public void XoaDHThua()
        {
            dh.XoaDHThua();
        }
    }
}
