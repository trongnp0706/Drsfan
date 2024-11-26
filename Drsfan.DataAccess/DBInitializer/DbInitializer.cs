using Drsfan.DataAccess.Data;
using Drsfan.Models;
using Drsfan.Utility;
using Drsfan.Utility.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.DataAccess.DBInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DrsfanDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(DrsfanDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!_roleManager.RoleExistsAsync(UserRoles.User).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRoles.User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Staff)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Company)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@email.com",
                    Email = "admin@email.com",
                    Name = "Admin",
                    PhoneNumber = "1112223333",
                    StreetAddress = "123 Admin St",
                    City = "Admin City",
                    PostalCode = "12345",
                    State = "Admin State"
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@email.com");
                _userManager.AddToRoleAsync(user, UserRoles.Admin).GetAwaiter().GetResult();

            }

            return;
        }
    }
}