using Microsoft.Extensions.Configuration;

namespace SmartLockDemo.Infrastructure.Utilities
{
    /// <summary>
    /// Provides various functionalities to configure an ASP.NET Core application
    /// </summary>
    public static class ConfigurationUtilities
    {
        /// <summary>
        /// Builds a configuration by compose appsetting.json files and environment variables
        /// </summary>
        /// <param name="basePath">Base path of the application</param>
        /// <param name="environment">Environment which will be configured</param>
        /// <returns></returns>
        public static IConfiguration BuildConfiguration(string basePath, string environment)
            => new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
