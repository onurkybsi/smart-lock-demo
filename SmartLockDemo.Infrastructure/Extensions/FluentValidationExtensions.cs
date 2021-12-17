using FluentValidation;
using FluentValidation.Results;
using System;
using System.Linq;

namespace SmartLockDemo.Infrastructure.Utilities
{
    /// <summary>
    /// Provides utility extensions for FluentValidation services
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Validates and throw an exception if the value invalid
        /// </summary>
        /// <typeparam name="T">Type of object to validate</typeparam>
        /// <param name="validator"></param>
        /// <param name="objectToValidate"></param>
        public static void ValidateWithExceptionOption<T>(this AbstractValidator<T> validator, T objectToValidate)
            => validator.Validate(objectToValidate, options => options.ThrowOnFailures());

        /// <summary>
        /// Extracts validation error messages from ValidationException and returns them in a string array
        /// </summary>
        /// <param name="validationException">Catched ValidationException</param>
        /// <returns>String array form of error messages</returns>
        /// <exception cref="ArgumentNullException">Parameter ValidationException and its Errors property cannot be null</exception>
        public static string[] ExtractErrorMessagesFromValidationException(this ValidationException validationException)
        {
            if (validationException is null)
                throw new ArgumentNullException(nameof(validationException));
            if (validationException.Errors is null)
                throw new ArgumentNullException(nameof(validationException.Errors));
            return validationException.Errors
                .Select(error => error.ErrorMessage)
                .ToArray();
        }

        /// <summary>
        /// Extracts validation error messages from ValidationResult and returns them in a string array
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Parameter ValidationResult and its Errors property cannot be null</exception>
        public static string[] ExtractErrorMessagesFromValidationResult(this ValidationResult validationResult)
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