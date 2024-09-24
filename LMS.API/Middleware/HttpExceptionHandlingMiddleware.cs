using LMS.Application.Exceptions;

using System.Text.Json;

namespace LMS.API.Middleware
{
    public class HttpExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpExceptionHandlingMiddleware> _logger;

        public HttpExceptionHandlingMiddleware(RequestDelegate next, ILogger<HttpExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Kör nästa middleware i pipelinen
                await _next(context);
            }
            catch (Exception ex)
            {
                // Hantera undantag här
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Standard respons om något oväntat inträffar
            int status; // = StatusCodes.Status500InternalServerError;
            string title; // = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });

            // Specifik hantering för olika undantagstyper
            switch (exception)
            {
                case NotFoundException:
                    status = StatusCodes.Status404NotFound;
                    //result = JsonSerializer.Serialize(new { error = "Resource not found." });
                    title = "Resource not found.";
                    break;
                case BadRequestException:
                    status = StatusCodes.Status400BadRequest;
                    title = "Bad request.";
                    break;
                case ConcurrencyException:
                    status = StatusCodes.Status409Conflict;
                    title = "Conflict, or Concurrency in database";
                    break;
                default:
                    // Standard respons om något oväntat inträffar
                    status = StatusCodes.Status500InternalServerError;
                    title = "An unexpected error occurred.";
                    break;
                    // Lägg till fler case för olika undantagstyper här
            }

            // Logga undantaget
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            // Skapa en responsstruktur
            var errorResponse = new
            {
                type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                title,
                detail = exception.Message,
                status,
                traceId = context.TraceIdentifier // Använder traceId från HttpContext
            };

            //type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            //    title = "Not Found",
            //    status = 404,
            //    traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            // Sätt responsens statuskod och innehåll
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            //context.Response.ContentLength = result.Length;
            //context.Response.Headers.Tra
            //return context.Response.WriteAsync(
            //    JsonSerializer.Serialize(errorResponse)
            //);

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentLength = jsonResponse.Length; // Uppdatera content-length
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
