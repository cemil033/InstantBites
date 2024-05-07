using InstantBites.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Seed
{
    public class IdentityDataSeeder
    {
        private readonly UserManager<Client> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataSeeder(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
                var result1 = await _roleManager.CreateAsync(new IdentityRole("Client"));
                if (!result1.Succeeded) throw new Exception(result.Errors.First().Description);
            }

            var user = await _userManager.FindByEmailAsync("admin@admin.com");

            if (user == null)
            {
                user = new Client
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Active = true,
                    Balance = 0,
                    CreatedTime = DateTime.Now,
                    ClientLocation = null,
                    Name = "Admin",
                    Surname = "Admin",
                    Id = Guid.NewGuid().ToString(),
                    PhoneNumber = "0",
                };
                var result = await _userManager.CreateAsync(user, "Admin!033");
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
                result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
                result = await _userManager.AddToRoleAsync(user, "Client");
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);

            }
        }

    }
}
