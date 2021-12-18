using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace SmartLockDemo.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = ConfigurationUtilities
                .BuildConfiguration(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            Log.Logger = CreateLogger(configuration);

            CreateHostBuilder(args, configuration).Build().Run();
        }

        private static Serilog.ILogger CreateLogger(IConfiguration configuration)
            => new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration).WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                        new Uri(configuration["ELASTICSEARCH_URI"]))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = string.Format("{0}-logs", configuration["APP_NAME"].ToLower())
                    }).CreateLogger();

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options => options.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => config.ClearProviders()).UseSerilog();
    }
}
