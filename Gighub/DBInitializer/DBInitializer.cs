using Gighub.Data;
using Gighub.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly AppDBContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DBInitializer(AppDBContext dbContext, UserManager<AppUser> userManager,
           RoleManager<IdentityRole> roleManager )
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initalize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                    _dbContext.Database.Migrate();
            }
            catch (Exception)
            {

                throw;
            }

            if (_dbContext.Roles.Any(r => r.Name == Helper.Admin)) return;

            
                 _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(Helper.Artist)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(Helper.Follower)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
               
            }, "Admin@123").GetAwaiter().GetResult() ;

            AppUser user = _dbContext.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();
        }
    }
}
