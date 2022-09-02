using FluentValidation;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Training.AspNet.Middlewares.App.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }        
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(exception)
            {
                case ValidationException validationExcaption:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationExcaption.Errors);
                    break;
                case NotFoundException validationExcaption:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType ="application/json";
            context.Response.StatusCode = (int)code;

            if(result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found.") { }
    }
    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
