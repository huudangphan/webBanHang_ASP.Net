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


    }
}
