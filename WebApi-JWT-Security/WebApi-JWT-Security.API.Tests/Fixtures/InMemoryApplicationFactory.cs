using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApi_JWT_Security.Persistence;

namespace WebApi_JWT_Security.API.Tests.Fixtures
{
    public class InMemoryApplicationFactory : WebApplicationFactory<Startup>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing")
                .ConfigureTestServices(services =>
                {
                    var options = new DbContextOptionsBuilder<WebApiDBContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<WebApiDBContext>();
                    db.Database.EnsureCreated();
                });
        }

    }
}
