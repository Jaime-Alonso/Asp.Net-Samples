using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Text;
using WebApi_JWT_Security.Configurations;
using WebApi_JWT_Security.Persistence;
using WebApi_JWT_Security.Services;

namespace WebApi_JWT_Security
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<WebApiDBContext>(c => c.UseInMemoryDatabase("WebApiInMemory"));

            var authSettings = Configuration.GetSection("AuthenticationSettings");
            services.Configure<AuthenticationSettings>(authSettings);

            var key = Encoding.ASCII.GetBytes(authSettings.Get<AuthenticationSettings>().Secret);

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<WebApiDBContext>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false                        
                    };
                });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi_JWT_Security", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebApiDBContext dbContext, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi_JWT_Security v1"));
                SeedExampleData(serviceProvider);
            }

            if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                dbContext.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SeedExampleData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                _ = SeedData.Initialize(services);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
    }
}
