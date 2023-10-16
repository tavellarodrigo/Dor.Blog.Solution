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

                // Log de la excepción (opcional)
                LogException(ex);

                // Configurar la respuesta de error
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
            // Puedes realizar la lógica de registro aquí
            // Por ejemplo, usando un sistema de registro como Serilog, log4net, etc.
        }
    }

}
