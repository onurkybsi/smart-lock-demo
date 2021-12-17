using FluentValidation;
using Microsoft.AspNetCore.Http;
using SmartLockDemo.Infrastructure.Utilities;
using System.Threading.Tasks;

namespace SmartLockDemo.WebAPI.Middlewares
{
    /// <summary>
    /// Handles exception when occurred executing REST services 
    /// </summary>
    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
            => _next = next;

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
            catch (System.Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                // TO-DO: Log it!
                await context.Response.WriteAsync("An exception occurred!");
            }
        }
    }
}
