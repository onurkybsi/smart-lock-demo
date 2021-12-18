using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace SmartLockDemo.Infrastructure.Utilities
{
    internal class EncryptionUtilities : IEncryptionUtilities
    {
        private readonly byte[] _salt;

        public EncryptionUtilities(byte[] salt)
            => _salt = salt;

        public string Hash(string valueToHash)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: valueToHash,
            salt: _salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
