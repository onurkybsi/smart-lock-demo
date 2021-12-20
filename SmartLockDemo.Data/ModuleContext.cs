using KybInfrastructure.Core;

namespace SmartLockDemo.Data
{
    /// <summary>
    /// Represents metadata of SmartLockDemo.Data module
    /// </summary>
    public class ModuleContext : IModuleContext
    {
        public string SQLServerConnectionString { get; }
        public string RedisUri { get; }
        public string RedisPort { get; }
        public bool IsCachingActive { get; set; }

        public ModuleContext(string sqlServerConnectionString, string redisUri,
            string redisPort, bool isCachingActive)
        {
            SQLServerConnectionString = sqlServerConnectionString;
            RedisUri = redisUri;
            RedisPort = redisPort;
            IsCachingActive = isCachingActive;
        }
    }
}
