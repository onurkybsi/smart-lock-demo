using FluentValidation;
using FluentValidation.Results;
using System;
using System.Linq;

namespace SmartLockDemo.Infrastructure.Utilities
{
    public static class FluentValidationExtensions
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

        /// <summary>
        /// Validates and throw an exception if the value invalid
        /// </summary>
        /// <typeparam name="T">Type of object to validate</typeparam>
        /// <param name="validator"></param>
        /// <param name="objectToValidate"></param>
        public static void ValidateWithExceptionOption<T>(this AbstractValidator<T> validator, T objectToValidate)
            => validator.Validate(objectToValidate, options => options.ThrowOnFailures());
    }
}