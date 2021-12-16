using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace SmartLockDemo.Infrastructure.Utilities
{
    internal class EncryptionUtilities : IEncryptionUtilities
    {
        private readonly byte[] salt;

        public EncryptionUtilities(byte[] salt)
        {
            this.salt = salt;
        }

        public string Hash(string valueToHash)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: valueToHash,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
