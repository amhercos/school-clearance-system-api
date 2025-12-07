using Microsoft.AspNetCore.Identity;
using Scs.Application.Interfaces;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IScsDbContext _context;

    public DataSeeder(RoleManager<IdentityRole<Guid>> roleManager,
                      UserManager<ApplicationUser> userManager,
                      IScsDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            await SeedRolesAsync(_roleManager);

            await SeedAdminUserAsync(_userManager);

        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager) 
        {
            string[] roles = { "Student", "Faculty", "Admin" };

            foreach (var roleName in roles)
            {
                if (await _roleManager.FindByNameAsync(roleName) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
        }
        private async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "admin@admin";
            const string adminPassword = "admin";

            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

