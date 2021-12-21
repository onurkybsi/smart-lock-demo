using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartLockDemo.Infrastructure.Utilities;
using System;
using System.Threading.Tasks;

namespace SmartLockDemo.WebAPI.Middlewares
{
    /// <summary>
    /// Handles exception when occurred executing REST services 
    /// </summary>
    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(ex.ExtractErrorMessagesFromValidationException());
            }
            catch (ArgumentNullException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Exception occurred when request proccessing: {0}", ex);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An exception occurred!");
            }
        }
    }
}
