using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SmartLockDemo.WebAPI.Middlewares;
using System.Security.Cryptography;

namespace SmartLockDemo.WebAPI
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
            DescribeModules(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartLockDemo.WebAPI", Version = "v1" });
            });
        }

        private static void DescribeModules(IServiceCollection services)
        {
            byte[] salt = new byte[128 / 8];
            using (RNGCryptoServiceProvider rngCsp = new()) rngCsp.GetBytes(salt);
            (new Infrastructure.ModuleDescriptor(new Infrastructure.ModuleContext(salt))).Describe(services);

            (new Data.ModuleDescriptor(new Data.ModuleContext("Server=(localdb)\\MSSQLLocalDB;Database=SmartLockDemo;Trusted_Connection=True;")))
                .Describe(services);
            (new Business.ModuleDescriptor()).Describe(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLockDemo.WebAPI v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
