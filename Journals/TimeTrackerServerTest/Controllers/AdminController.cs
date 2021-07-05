using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using JournalsServer.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace JournalsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            int Id = UserController.isValidJWT(Request);
            if ((Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0) || AppDbContext.db.Users.ToList().Count == 0)
            {
                if (ModelState.IsValid)
                {
                    User user = await AppDbContext.db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                    if (user == null)
                    {
                        // добавляем пользователя в бд
                        if (model.Password.Length < 4)
                        {
                        }
                        SHA512Managed sha = new SHA512Managed();
                        string hash = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
                        AppDbContext.db.Users.Add(new User { Login = model.Login, Password = hash, Name = model.Name, Position = model.Position });
                        await AppDbContext.db.SaveChangesAsync();
                        return Ok();
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return UnprocessableEntity();
            }
            return Forbid();
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserController.LoginInfo model)
        {
            int Id = UserController.isValidJWT(Request);
            if ((Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0) || AppDbContext.db.Users.ToList().Count == 0)
            {
                if (ModelState.IsValid)
                {
                    User user = await AppDbContext.db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                    if (user == null)
                    {
                        SHA512Managed sha = new SHA512Managed();
                        user.Password = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
                        await AppDbContext.db.SaveChangesAsync();
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return UnprocessableEntity();
            }
            return Forbid();
        }

        [Route("GetWorkers")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetWorkers()
        {
            Console.WriteLine("\r\n\r\n\r\nНачнем\r\n\r\n\r\n");
            int Id = UserController.isValidJWT(Request);
            Console.WriteLine("\r\n\r\n\r\nId = {0}\r\n\r\n\r\n", Id);
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return AppDbContext.db.Users.ToList().ConvertAll(t=>new { login = t.Login, name = t.Name, Position = t.Position});
            }
            return Forbid();
        }
    }
}
