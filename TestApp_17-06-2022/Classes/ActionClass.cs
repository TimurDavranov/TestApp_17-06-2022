using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp_17_06_2022.Data;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Models;

namespace TestApp_17_06_2022.Classes
{
    public class ActionClass : IActionClass<User>
    {
        private readonly IAppDbContext _db;
        
        public ActionClass(IAppDbContext db)
        {
            _db = db;
        }
        
        public async Task<User> Login(LoginViewModel model)
        {
            var result = await _db.Users.Where(s => s.Login.ToLower() == model.Login.ToLower() && s.Password == model.Password).ToListAsync();
            if (result.Count > 0)
                return result.FirstOrDefault();
            else return null;
        }

        public async Task<User> Signin(LoginViewModel model)
        {
            var entity = new User()
            {
                Login = model.Login,
                Password = model.Password
            };
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}