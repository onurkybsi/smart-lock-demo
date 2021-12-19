using KybInfrastructure.Core;

namespace SmartLockDemo.Infrastructure
{
    /// <summary>
    /// Represents metadata of SmartLockDemo.Infrastructure module
    /// </summary>
    public class ModuleContext : IModuleContext
    {
        public byte[] HashingSalt { get; }
        public string BearerTokenSecurityKey { get; }
        public int ValidityPeriodOfBearerTokenInMs { get; }

        public ModuleContext(byte[] hashingSalt, string bearerTokenSecurityKey, int validityPeriodOfBearerTokenInMs)
        {
            HashingSalt = hashingSalt;
            BearerTokenSecurityKey = bearerTokenSecurityKey;
            ValidityPeriodOfBearerTokenInMs = validityPeriodOfBearerTokenInMs;
        }
    }
}
