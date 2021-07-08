using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalsServer.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComanderController : ControllerBase
    {

        [Route("ConfirmRegister/{id}")]
        [HttpPost]
        public async Task<IActionResult> ConfirmRegister(ulong id)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            User comander = AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id);
            if (comander != null && comander.Position < 0)
            {
                if (ModelState.IsValid)
                {
                    Registration registration = await AppDbContext.db.Registrations.FirstOrDefaultAsync(u => u.Id == id);
                    if (registration == null)
                    {
                        AppDbContext.db.Registrations.Remove(registration);
                        AppDbContext.db.Users.Add(new User { Login = registration.Login, Name = registration.Name, Password = registration.Password, Position = Math.Abs(comander.Position)});
                        await AppDbContext.db.SaveChangesAsync();
                        return Ok();
                    }
                    else
                        return UnprocessableEntity("Логин уже занят");
                }
                return UnprocessableEntity();
            }
            return Forbid();
        }

    }
}
