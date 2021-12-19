using Microsoft.AspNetCore.Http;
using SmartLockDemo.Business.Service.User;
using System;
using System.Linq;
using System.Security.Claims;

namespace SmartLockDemo.WebAPI.Utilities
{
    internal static class Extensions
    {
        private const string USER_ID_CLAIM_KEY = "ID";

        /// <summary>
        /// Receives requesting user ID from authorization claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">It is thrown if given context is null</exception>
        /// <exception cref="InvalidOperationException">It is thrown if the user ID claim couldn't received</exception>
        public static int GetUserId(this HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Claim userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == USER_ID_CLAIM_KEY);
            if (string.IsNullOrWhiteSpace(userIdClaim?.Value))
                throw new InvalidOperationException("User ID claim couldn't received!");

            return int.Parse(userIdClaim.Value);
        }

        /// <summary>
        /// Receives requesting user email from authorization claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">It is thrown if given context is null</exception>
        /// <exception cref="InvalidOperationException">It is thrown if the user email claim couldn't received</exception>
        public static string GetUserEmail(this HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Claim userEmailClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(userEmailClaim?.Value))
                throw new InvalidOperationException("User email claim couldn't received!");

            return userEmailClaim.Value;
        }

        /// <summary>
        /// Receives requesting user role from authorization claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">It is thrown if given context is null</exception>
        /// <exception cref="InvalidOperationException">It is thrown if the user role claim couldn't received</exception>
        public static Role GetUserRole(this HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Claim userRoleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (string.IsNullOrWhiteSpace(userRoleClaim?.Value))
                throw new InvalidOperationException("User role claim couldn't received!");

            return UserUtilities.ConvertToRole(userRoleClaim.Value);
        }
    }
}
