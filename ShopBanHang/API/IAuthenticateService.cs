using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    interface IAuthenticateService
    {
        User Authenticate(string username, string password);
        List<string> Token();
    }
}
