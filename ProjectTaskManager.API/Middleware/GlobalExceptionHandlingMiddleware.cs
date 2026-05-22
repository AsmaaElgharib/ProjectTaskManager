using System.Net;
using System.Text.Json;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Models;
using ValidationException = ProjectTaskManager.Application.Common.Exceptions.ValidationException;


namespace ProjectTaskManager.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, response) = exception switch
            {
                ValidationException ve => (
                    HttpStatusCode.BadRequest,
                    ApiResponse.FailureResult("Validation failed.", ve.Errors.SelectMany(e => e.Value))),

                NotFoundException => (
                    HttpStatusCode.NotFound,
                    ApiResponse.FailureResult(exception.Message)),

                UnauthorizedException => (
                    HttpStatusCode.Forbidden,
                    ApiResponse.FailureResult(exception.Message)),

                ConflictException => (
                    HttpStatusCode.Conflict,
                    ApiResponse.FailureResult(exception.Message)),

                _ => (
                    HttpStatusCode.InternalServerError,
                    ApiResponse.FailureResult("An unexpected error occurred. Please try again later."))
            };

            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
