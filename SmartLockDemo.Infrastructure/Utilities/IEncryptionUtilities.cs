namespace SmartLockDemo.Infrastructure.Utilities
{
    public interface IEncryptionUtilities
    {
        string Hash(string valueToHash);
    }
}
