using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query.DonHangQuery;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers.DonHang
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HDTGController : ControllerBase
    {
        DHTGQuery dh = new DHTGQuery();
        //[HttpGet]
        //public string getTien(int mahd)
        //{
        //    return dh.tongTien(mahd);
        //}
        [HttpPost]
        public void TaoHDTG(int makh,double tiencoc,int sothang)
        {
            dh.TaoHDTG(makh, tiencoc, sothang);
        }
        [HttpGet]
        public string test()
        {
            return dh.getmahd();
        }
        [HttpPost]
        public void TaoCTHDTG( int makho,int masp,int sl,double giaban)
        {
            dh.TaoCTTG( makho, masp, sl, giaban);
        }
        [HttpGet]
        public string GetHDTG()
        {
            return dh.getHDTG();
        }
        [HttpGet]
        public string GetCTHDTG(int mahd)
        {
            return dh.GetCTHDTG(mahd);
        }
        [HttpGet]
        public string GetHDTGId(int mahd)
        {
            return dh.GetHDTGId(mahd);
        }

        [HttpGet]
        public string GetPTG(int mahd)
        {
            return dh.GetPTG(mahd);
        }
        [HttpGet]
        public string GetCTPTG(int maphieu)
        {
            return dh.GetCTPTG(maphieu);
        }
        [HttpPost]
        public void TraGop(int maphieu)
        {
            dh.TraGop(maphieu);
        }
        [HttpPut]
        public void updateMaHD()
        {
            dh.updatemadh();
        }
        [HttpPost]
        public void UpdateNgayTra()
        {
            dh.autoupdate();
        }
    }
}
