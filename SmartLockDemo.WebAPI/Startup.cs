using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using SmartLockDemo.WebAPI.Middlewares;
using System.Security.Cryptography;

namespace SmartLockDemo.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            DescribeModules(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartLockDemo.WebAPI", Version = "v1" });
            });
        }

        private void DescribeModules(IServiceCollection services)
        {
            (new Infrastructure.ModuleDescriptor(new Infrastructure.ModuleContext(Configuration.GetSection("HASHING_SALT").Get<byte[]>()))).Describe(services);

            (new Data.ModuleDescriptor(new Data.ModuleContext(Configuration["MSSQL_CONNECTION_STRING"])))
                .Describe(services);
            (new Business.ModuleDescriptor()).Describe(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLockDemo.WebAPI v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.ForContext<Startup>().Information("{Application} is listening on {Env}...", env.ApplicationName, env.EnvironmentName);
        }
    }
}
