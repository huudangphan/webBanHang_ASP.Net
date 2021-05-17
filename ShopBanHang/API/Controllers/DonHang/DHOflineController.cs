using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Query.DonHangQuery;

namespace API.Controllers.DonHang
{
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
    }
}
