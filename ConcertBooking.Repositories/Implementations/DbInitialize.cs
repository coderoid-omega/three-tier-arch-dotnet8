using ConcertBooking.Entities;
using ConcertBooking.Infrastructure;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Implementations
{
    public class DbInitialize : IDbInitialize
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!await _roleManager.RoleExistsAsync(GlobalConfiguration.Admin_Role))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = GlobalConfiguration.Admin_Role });
                var user = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com", EmailConfirmed = true };
                await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, GlobalConfiguration.Admin_Role);
            }
            if (!await _roleManager.RoleExistsAsync(GlobalConfiguration.User_Role))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = GlobalConfiguration.User_Role });
                var user = new ApplicationUser { Email = "testuser@gmail.com", UserName = "testuser@gmail.com", EmailConfirmed = true };
                await _userManager.CreateAsync(user, "User@123");
                await _userManager.AddToRoleAsync(user, GlobalConfiguration.User_Role);
            }
        }
    }
}
