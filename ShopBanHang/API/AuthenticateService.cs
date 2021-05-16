using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Odbc;
using API.Global;
using System.Data;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSetting _appSettings;
        public AuthenticateService(IOptions<AppSetting> appsttings)
        {
            _appSettings = appsttings.Value;
        }
        DataTable dt;
        private string ExcuteQuery(string query,string para)
        {
            OdbcConnection conn = new OdbcConnection(GlobalData.conStr);
            OdbcCommand cmd = new OdbcCommand(query, conn);
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
            dt = new DataTable();
            string result = "";
            try
            {
                conn.Open();
                adapter.Fill(dt);
                OdbcDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {
                    result = dRead[para].ToString();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }
        private string GetUserName(string username,string password)
        {
            string query = "select userAdmin from Admin where userAdmin='" + username + "' and passAdmin='" + password + "'";
            return ExcuteQuery(query,"userAdmin");
        }
        private string GetPassword(string username, string password)
        {
            string query = "select passAdmin from Admin where userAdmin='" + username + "' and passAdmin='" + password + "'";
            return ExcuteQuery(query,"passAdmin");
        }
        private string GetRole(string username, string password)
        {
            string query = "select Loai from Admin where userAdmin='" + username + "' and passAdmin='" + password + "'";
            return ExcuteQuery(query,"Loai");
        }
        public User Authenticate(string username, string password)
        {
            string uName = GetUserName(username, password);
            string pWord = GetPassword(username, password);
            string loai = GetRole(username, password);
            List<User> ListUser = new List<User>()
            {
                new User{username=uName,password=pWord,loai=loai}
            };
            // tao token
            var user = ListUser.SingleOrDefault(x => x.username == username && x.password == password);
            if (user == null)

                return null;
           
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                 {   new Claim("username",user.username.ToString()),
                     new Claim(ClaimTypes.Role,"Admin"),
                     new Claim(ClaimTypes.Version,"V3.1")
                 }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            user.password = "";
            var token = tokenHandle.CreateToken(tokenDescriptor);
            string tokenRes = tokenHandle.WriteToken(token);

            user.token = tokenRes;
            
            Startup.listToken.Add(tokenRes);
            return user;
            
        }

        public List<string> Token()
        {
            return Startup.listToken;   
        }
    }
}
