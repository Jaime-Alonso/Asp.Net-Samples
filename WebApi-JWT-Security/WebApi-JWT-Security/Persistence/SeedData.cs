using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApi_JWT_Security.Persistence
{
    public static class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync("Jaime");
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "Jaime",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "secretPassword@123");
            }

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var rol = await roleManager.FindByNameAsync("User");

            if (rol == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            await userManager.AddToRoleAsync(user, "User");

        }
    }
}
