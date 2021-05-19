﻿using API.Query;
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
    }
}
