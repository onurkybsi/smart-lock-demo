using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SmartLockDemo.WebAPI.Middlewares;
using System.Text;

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
            ConfigureAuthorization(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartLockDemo.WebAPI", Version = "v1" });
            });
        }

        private void DescribeModules(IServiceCollection services)
        {
            (new Infrastructure.ModuleDescriptor(new Infrastructure.ModuleContext(Configuration.GetSection("HASHING_SALT").Get<byte[]>(),
                Configuration["TOKEN_SECRET_KEY"], Configuration.GetSection("VALIDITY_PERIOD_OF_TOKENS_IN_MIN").Get<int>())))
                .Describe(services);

            (new Data.ModuleDescriptor(new Data.ModuleContext(Configuration["MSSQL_CONNECTION_STRING"]), Configuration["ADMIN_EMAIL"],
                Configuration["ADMIN_HASHED_PASSWORD"]))
                .Describe(services);

            (new Business.ModuleDescriptor()).Describe(services);
        }

        private IServiceCollection ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TOKEN_SECRET_KEY"])),
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLockDemo.WebAPI v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.ForContext<Startup>().Information("{Application} is listening on {Env}...", env.ApplicationName, env.EnvironmentName);
        }
    }
}
