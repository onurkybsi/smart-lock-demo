namespace SmartLockDemo.Infrastructure.Utilities
{
    public interface IEncryptionUtilities
    {
        /// <summary>
        /// Creates hashed form of given value using given salt in the module context
        /// </summary>
        /// <param name="valueToHash">Value to hash</param>
        /// <returns></returns>
        string Hash(string valueToHash);

        /// <summary>
        /// Validates given value whether hashed by salt given in the module context
        /// </summary>
        /// <param name="hashedValue">Hashed value to validate</param>
        /// <returns></returns>
        bool ValidateHashedValue(string hashedValue);

        /// <summary>
        /// Creates bearer token for a user of a system using the secret key given in the module context
        /// </summary>
        /// <returns>Created token</returns>
        string CreateBearerToken();

        /// <summary>
        /// Creates bearer token for a user of a system using the secret key given in the module context
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Created token</returns>
        string CreateBearerToken(BearerTokenCreationRequest request);
    }
}
