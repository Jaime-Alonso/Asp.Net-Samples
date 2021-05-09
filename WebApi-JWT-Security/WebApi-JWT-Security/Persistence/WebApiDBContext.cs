using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi_JWT_Security.Entities;

namespace WebApi_JWT_Security.Persistence
{
    public class WebApiDBContext : IdentityDbContext<IdentityUser>
    {
        public WebApiDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
