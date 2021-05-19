﻿using Microsoft.AspNetCore.Http;
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
        [HttpPost("{makh}")]
        public void TaoHDOffline(int makh)
        {
            dh.TaoDHOffline(makh);
        }
        [HttpPost]
        public void TaoCTHDOff(int mahd,int masp,double giaBan,int SL,int makho)
        {
            dh.TaoCTDOff(makho, mahd, masp, SL, giaBan);
        }
        [HttpGet]
        public string HDOff(int makh)
        {
            return dh.HDOff(makh);
        }
    }
}
