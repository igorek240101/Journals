using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Cryptography;
using JournalsServer.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace JournalsServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginInfo login)
        {
            if (ModelState.IsValid)
            {
                SHA512Managed sha = new SHA512Managed();
                string hash = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(login.Password)));
                User user = await AppDbContext.db.Users.FirstOrDefaultAsync(u => u.Login == login.Login && u.Password == hash);
                if (user != null)
                {
                    return Ok(new { token = GenerateJWT(user)});
                }
            }
            return UnprocessableEntity();
        }

        [Route("ResetToken")]
        [HttpGet]
        private ActionResult ResetToken()
        {
            int Id =  isValidJWT(Request);
            if (Id != -1)
            {
                return Ok(new { token = GenerateJWT(AppDbContext.db.Users.Find(Id)) });
            }
            return Unauthorized();
        }

        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>()
            {
                new Claim("jti",user.Id.ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(ServerInfo.Issuer, ServerInfo.Audience, claims, DateTime.Now, DateTime.Now.AddSeconds(ServerInfo.TokenLifetime), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static int isValidJWT(HttpRequest request)
        {
            if (request.IsHttps)
            {
                IHeaderDictionary header = request.Headers;
                string s = header["Authorization"];
                if (s != null)
                {
                    JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
                    if (s.Length > 7 && jwt.CanReadToken(s.Substring(7)))
                    {
                        JwtSecurityToken token = jwt.ReadJwtToken((s).Substring(7));
                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                        var claims = new List<Claim>()
                        {
                            new Claim("jti",token.Id)
                        };
                        JwtSecurityToken validtoken = new JwtSecurityToken(token.Issuer, token.Audiences.First(), claims, token.ValidFrom, token.ValidTo, credentials);
                        validtoken = (new JwtSecurityTokenHandler()).ReadJwtToken(new JwtSecurityTokenHandler().WriteToken(validtoken));
                        if (token.Issuer == ServerInfo.Issuer && token.Audiences.First() == ServerInfo.Audience && token.RawSignature == validtoken.RawSignature && token.ValidTo < DateTime.Now)
                        {
                            return Convert.ToInt32(token.Id);
                        }
                    }
                }
            }
            return -1;
        }



        public struct LoginInfo
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private static class ServerInfo
        {
            public static readonly string Issuer = "Igor";

            public static readonly string Audience = "Alexandr";

            public static readonly int TokenLifetime = 3600;

            public static string Secret;

            static ServerInfo()
            {
                FileStream file = new FileStream("Secret.9370", FileMode.Open);
                StreamReader reader = new StreamReader(file);
                Secret = reader.ReadToEnd();
                reader.Close();
                file.Close();
            }
        }
    }
}
