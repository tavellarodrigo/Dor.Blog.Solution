using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dor.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        //private readonly ILoggerUtil<ExceptionHandlingMiddleware> _logger;        
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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

                // Respuesta JSON personalizada
                var errorResponse = new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }

        }
        private void LogException(Exception exception)
        {
            
            // Serilog, log4net
        }
    }

}
