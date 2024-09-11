using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TechsysLog.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
           
            _logger.LogError(exception, exception.Message);

          
            var statusCode = HttpStatusCode.InternalServerError;

          
            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            var response = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                Details = exception.StackTrace 
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }

}
