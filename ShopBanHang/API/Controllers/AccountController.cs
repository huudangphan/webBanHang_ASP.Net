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
    public class AccountController : ControllerBase
    {
        
        private IAuthenticateService _authenticateService;
        public AccountController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        AccountQuery acc = new AccountQuery();
        [HttpGet]
        public string getAccount()
        {
            return acc.getAccount();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            var user = _authenticateService.Authenticate(model.username, model.password);
            if (user == null)
                return BadRequest();
            return Ok(user);

        }
    }
}
