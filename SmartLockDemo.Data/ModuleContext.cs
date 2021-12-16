using KybInfrastructure.Core;

namespace SmartLockDemo.Data
{
    /// <summary>
    /// Represents metadata of SmartLockDemo.Data module
    /// </summary>
    public class ModuleContext : IModuleContext
    {
        public string SQLServerConnectionString { get; }

        public ModuleContext(string sqlServerConnectionString)
            => SQLServerConnectionString = sqlServerConnectionString;
    }
}
