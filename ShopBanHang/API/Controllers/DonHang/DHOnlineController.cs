using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query.DonHangQuery;
using API.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers.DonHang
{
    //[APIAuthorization]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DHOnlineController : ControllerBase
    {
        DHOnlineQuery dh = new DHOnlineQuery();
        //[HttpGet]
        //public string GetDHOnline()
        //{
        //    return dh.GetHDOnline();
        //}
        [HttpGet("{madh}")]
        public string GetCTHDOnline(int mahd)
        {
            return dh.GetCTHDOnline(mahd);
        }
       [HttpPost]
       public int GuiHang(int MaDH,int MaKho)
        {
            return dh.GuiHangOnline(MaDH, MaKho);
        }

    }
}
