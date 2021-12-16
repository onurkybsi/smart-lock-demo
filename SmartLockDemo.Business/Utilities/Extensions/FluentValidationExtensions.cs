using FluentValidation.Results;
using System;
using System.Linq;

namespace SmartLockDemo.Business.Utilities
{
    internal static class FluentValidationExtensions
    {
        public static String[] ExtractErrorMessagesFromErrors(this ValidationResult validationResult)
        {
            if (validationResult is null)
                throw new ArgumentNullException(nameof(validationResult));
            if (validationResult.Errors is null)
                throw new ArgumentNullException(nameof(validationResult.Errors));
            return validationResult.Errors
                .Select(error => error.ErrorMessage)
                .ToArray();
        }
    }
}
