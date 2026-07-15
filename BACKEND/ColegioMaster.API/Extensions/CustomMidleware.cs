using ColegioMaster.DtoModels.Comp;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ColegioMaster.API.Extensions
{
    public class CustomMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public CustomMidleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //// 1. Intentar obtener el header enviado por el cliente
                //if (!context.Request.Headers.TryGetValue("codigo-aplicacion", out var codigoAplicacion)
                //    || string.IsNullOrWhiteSpace(codigoAplicacion))
                //{
                //    // Si no viene o está vacío, disparamos una excepción personalizada o manejamos el error directo
                //    await HandleBadRequestAsync(context, "El encabezado 'codigo-aplicacion' es obligatorio.");
                //    return; // Cortamos el flujo aquí, no llega a los controladores
                //}

                //// 2. [Opcional] Si necesitas usar este código en tus Controllers o Servicios más adelante,
                //// puedes guardarlo en los Items del contexto para recuperarlo después.
                //context.Items["CodigoAplicacion"] = codigoAplicacion.ToString();

                ////3 vamos a validar el código de aplicación
                //if(codigoAplicacion != "123456")
                //{
                //    await HandleBadRequestAsync(context, "Hey chochera, código de aplicación incorrecta");
                //    return; // Cortamos el flujo aquí, no llega a los controladores
                //}

                // 4. Continúa el flujo normal si todo está en orden
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si algo falla en los controladores o servicios, cae aquí
                _logger.LogError(ex, "Ha ocurrido un error no controlado en la aplicación.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // Método auxiliar para responder rápidamente si falta el header obligatorio
        private static Task HandleBadRequestAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest; // 400

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }



        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Por defecto devolvemos un 500 (Internal Server Error)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Estructura de respuesta que verá el cliente
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno del servidor. Inténtelo más tarde.",
                Detail = exception.Message // Ojo: En producción es mejor no mostrar el detalle exacto por seguridad
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}