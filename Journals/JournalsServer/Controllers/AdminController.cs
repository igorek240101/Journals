using JournalsServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;

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
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if ((Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0) || AppDbContext.db.Users.ToList().Count == 0)
            {
                if (ModelState.IsValid)
                {
                    User user = await AppDbContext.db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                    if (user == null)
                    {
                        if (AppDbContext.db.Departments.FirstOrDefault(u => u.Id == Math.Abs(model.Position)) == null
                            && model.Position != 0) return UnprocessableEntity("Такой отдел не зарегестрирован");
                        if (model.Position < 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Position == model.Position) != null) return UnprocessableEntity("У этого отдела уже есть руководитель");
                        if (model.Password.Length < 4)
                        {
                            return UnprocessableEntity("Слишком короткий пароль");
                        }
                        SHA512Managed sha = new SHA512Managed();
                        string hash = System.Text.Encoding.UTF8.GetString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
                        AppDbContext.db.Users.Add(new User { Login = model.Login, Password = hash, Name = model.Name, Position = model.Position });
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

        [Route("GetRegistration")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetRegistration()
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return AppDbContext.db.Registrations.ToList().ConvertAll(t => new { Id = t.Id, Name = t.Name });
            }
            return Forbid();
        }

        [Route("DeleteRegistration/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeketetRegistration(ulong id)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                Registration registration = AppDbContext.db.Registrations.FirstOrDefault(t => t.Id == id);
                if (registration != null)
                {
                    AppDbContext.db.Registrations.Remove(registration);
                    await AppDbContext.db.SaveChangesAsync();
                    return Ok();
                }
                return UnprocessableEntity();
            }
            return Forbid();
        }

        [Route("GetWorkers")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetWorkers()
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return AppDbContext.db.Users.ToList().ConvertAll(t => new { login = t.Login, name = t.Name, Position = t.Position });
            }
            return Forbid();
        }

        [Route("CreateDepartment")]
        [HttpPost]
        public async Task<ActionResult> CreateDepartment(Department department)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                if (AppDbContext.db.Departments.FirstOrDefault(u => u.Id == department.Id) != null) return UnprocessableEntity("Отдел с таким номером уже зарегестирован");
                AppDbContext.db.Departments.Add(department);
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Forbid();
        }

        [Route("RenameDepartment")]
        [HttpPut]
        public async Task<ActionResult> RenameDepartment(Department department)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                Department departmentold = AppDbContext.db.Departments.FirstOrDefault(u => u.Id == department.Id);
                if (department == null) return UnprocessableEntity("Отдел с таким номером не существует");
                departmentold.Name = department.Name;
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Forbid();
        }

        [Route("GetDepartment")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetDepartments()
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return AppDbContext.db.Departments.ToList().ConvertAll(t => new { login = t.Id, name = t.Name });
            }
            return Forbid();
        }

        [Route("GetDepartment/{id}")]
        [HttpGet]
        public ActionResult GetDepartment(sbyte id)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return Ok(AppDbContext.db.Departments.FirstOrDefault(u => u.Id == id));
            }
            return Forbid();
        }

        [Route("DeleteDepartment")]
        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(sbyte id)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                Department department = AppDbContext.db.Departments.FirstOrDefault(u => u.Id == id);
                if (department == null) return UnprocessableEntity("Отдел с таким номером не существует");
                if (AppDbContext.db.Users.FirstOrDefault(u => Math.Abs(u.Position) == department.Id) != null) return UnprocessableEntity("Нельзя удаллить отдел с работниками");
                AppDbContext.db.Departments.Remove(department);
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Forbid();
        }


        [Route("ChangeDepartment")]
        [HttpPut]
        public async Task<IActionResult> ChangeDepartment((string, sbyte) model)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if ((Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0) || AppDbContext.db.Users.ToList().Count == 0)
            {
                if (ModelState.IsValid)
                {
                    User user = await AppDbContext.db.Users.FirstOrDefaultAsync(u => u.Login == model.Item1);
                    if (user == null)
                    {
                        if (AppDbContext.db.Departments.FirstOrDefault(u => u.Id == Math.Abs(model.Item2)) == null
                            && model.Item2 != 0) return UnprocessableEntity("Такой отдел не зарегестрирован");
                        if (model.Item2 < 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Position == model.Item2) != null) return UnprocessableEntity("У этого отдела уже есть руководитель");
                        user.Position = model.Item2;
                        await AppDbContext.db.SaveChangesAsync();
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин");
                }
                return UnprocessableEntity();
            }
            return Forbid();
        }

        [Route("CreateShablon/{id}")]
        [HttpPost]
        public async Task<ActionResult> CreateShablon(sbyte id, IFormFile file)
        {
            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                using (var stream = System.IO.File.Create("Resource\\Shablons\\" + file.FileName))
                {
                    await file.CopyToAsync(stream);
                }
                Department department = AppDbContext.db.Departments.FirstOrDefault(t => t.Id == id);
                if (department == null) return BadRequest("Такой отдел не найден");
                AppDbContext.db.JournalShablons.Add(new JournalShablons { Name = file.FileName, Department = department });
                await AppDbContext.db.SaveChangesAsync();
                return Ok();
            }
            return Forbid();
        }

        [Route("GetShablons")]
        [HttpGet]
        public ActionResult<IEnumerable<JournalShablons>> GetShablons()
        {

            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                return AppDbContext.db.JournalShablons.ToList();
            }
            return Forbid();
        }

        [Route("GetShablon/{id}")]
        [HttpGet]
        public ActionResult<FileResult> GetShablon(ulong id)
        {

            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                JournalShablons shablons = AppDbContext.db.JournalShablons.FirstOrDefault(t => t.Id == id);
                if (shablons == null) return BadRequest("Шаблон не найден");
                FileStream file = new FileStream("Resource\\Shablons\\" + shablons.Name, FileMode.Open);
                return Ok(File(file, "xml", shablons.Name));
            }
            return Forbid();
        }

        [Route("DeleteShablon/{id}")]
        [HttpDelete]
        public ActionResult DeleteShablon(ulong id)
        {

            ulong Id;
            try
            {
                Id = UserController.isValidJWT(Request);
            }
            catch { return Unauthorized(); }
            if (Id > 0 && AppDbContext.db.Users.FirstOrDefault(u => u.Id == Id).Position == 0)
            {
                JournalShablons shablons = AppDbContext.db.JournalShablons.FirstOrDefault(t => t.Id == id);
                if (shablons == null) return BadRequest("Шаблон не найден");
                System.IO.File.Delete("Resource\\Shablons\\" + shablons.Name);
                AppDbContext.db.JournalShablons.Remove(shablons);
                AppDbContext.db.SaveChanges();
                return Ok();
            }
            return Forbid();
        }
    }
}
