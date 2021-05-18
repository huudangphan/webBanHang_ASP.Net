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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public void TaoCTHDTG(int makho,int masp,int sl,double giaban)
        {
            dh.TaoCTTG(makho, masp, sl, giaban);
        }

    }
}
