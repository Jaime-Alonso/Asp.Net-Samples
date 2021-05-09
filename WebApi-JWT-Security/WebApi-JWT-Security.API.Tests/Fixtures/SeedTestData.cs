using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi_JWT_Security.API.Tests.Fixtures
{
    public static class SeedTestData
    {
        public static async Task CreateUsersAsync(InMemoryApplicationFactory factory)
        {
            
            var userManager = factory.Services.GetService<UserManager<IdentityUser>>();

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
        }
    }
}
