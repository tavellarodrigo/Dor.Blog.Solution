using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Dor.Middleware
{
    /// <summary>
    /// any unhandled exceptions are entered here and log the error message
    /// </summary>
    public class ExceptionHandlingMiddleware
    {   
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                var exceptionHandlerFeature = new ExceptionHandlerFeature
                {
                    Error = ex,
                    Path = context.Request.Path
                };

                context.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);
                
                LogException(ex);
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                
                var errorResponse = new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }

        }
        private void LogException(Exception e)
        {
            // Serilog
            _logger.LogCritical(String.Join(" ","Critical error " ,e.Message));
        }
    }

}
