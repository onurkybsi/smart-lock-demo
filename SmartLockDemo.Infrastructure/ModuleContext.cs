using KybInfrastructure.Core;

namespace SmartLockDemo.Infrastructure
{
    /// <summary>
    /// Represents metadata of SmartLockDemo.Infrastructure module
    /// </summary>
    public class ModuleContext : IModuleContext
    {
        public byte[] HashingSalt { get; }

        public ModuleContext(byte[] hashingSalt)
        {
            this.HashingSalt = hashingSalt;
        }
    }
}
