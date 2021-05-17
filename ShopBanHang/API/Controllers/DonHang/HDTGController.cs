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
    }
}
