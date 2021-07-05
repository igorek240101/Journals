using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalsServer.Model;

namespace JournalsServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return AppDbContext.db.Users.Where(t => true);
        }

        [HttpPost]
        public void Post(User user)
        {
            AppDbContext.db.Users.Add(user);
            AppDbContext.db.SaveChangesAsync();
        }
    }
}
