using API.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.NhanVien
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        NhanVienQuery nv = new NhanVienQuery();
        [HttpGet]
        public string getNhanVien()
        {
            return nv.GetNhanVien();
        }
    }
}
