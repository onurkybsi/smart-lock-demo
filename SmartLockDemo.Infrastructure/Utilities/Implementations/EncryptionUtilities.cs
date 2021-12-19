using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartLockDemo.Infrastructure.Utilities
{
    internal class EncryptionUtilities : IEncryptionUtilities
    {
        private readonly byte[] _saltWhichWillBeUsedInHashing;
        private readonly string _secretKeyWhichWillBeUsedInTokenCreation;
        private readonly int _expireDateOfTokensWhichWillBeCreated;

        public EncryptionUtilities(byte[] saltWhichWillBeUsedInHashing,
                                   string secretKeyWhichWillBeUsedInTokenCreation,
                                   int expireDateOfTokensWhichWillBeCreated)
        {
            _saltWhichWillBeUsedInHashing = saltWhichWillBeUsedInHashing;
            _secretKeyWhichWillBeUsedInTokenCreation = secretKeyWhichWillBeUsedInTokenCreation;
            _expireDateOfTokensWhichWillBeCreated = expireDateOfTokensWhichWillBeCreated;
        }

        public string Hash(string valueToHash)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: valueToHash,
            salt: _saltWhichWillBeUsedInHashing,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        public bool ValidateHashedValue(string hashedValue)
        {
            throw new NotImplementedException();
        }

        public string CreateBearerToken()
            => CreateBearerToken(default);

        public string CreateBearerToken(BearerTokenCreationRequest request)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            byte[] secretKey = Encoding.ASCII.GetBytes(_secretKeyWhichWillBeUsedInTokenCreation);
            var tokenDescriptor = SetTokenSubjectByRequest(new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(0.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            }, request);

            SecurityToken createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

        private static SecurityTokenDescriptor SetTokenSubjectByRequest(SecurityTokenDescriptor tokenDescriptor,
            BearerTokenCreationRequest request)
        {
            if (request is null)
                return tokenDescriptor;

            List<Claim> claims = new();
            if (request.Id.HasValue)
                claims.Add(new Claim("ID", request.Id.Value.ToString()));
            if (!string.IsNullOrWhiteSpace(request.Email))
                claims.Add(new Claim(ClaimTypes.Email, request.Id.Value.ToString()));
            if (!string.IsNullOrWhiteSpace(request.Role))
                claims.Add(new Claim(ClaimTypes.Role, request.Role));

            if (claims.Any())
                tokenDescriptor.Subject = new ClaimsIdentity(claims);

            return tokenDescriptor;
        }
    }
}
