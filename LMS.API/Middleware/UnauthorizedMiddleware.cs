
namespace LMS.API.Middleware
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Kör nästa middleware först
            await _next(context);

            // Kolla om statuskoden är 401
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                // Du kan här hantera 401-svaret
                await HandleUnauthorizedAsync(context);
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                // Du kan här hantera 401-svaret
                await HandleForbiddenAsync(context);
            }
        }

        private Task HandleUnauthorizedAsync(HttpContext context)
        {
            // Anpassa meddelandet för 401-svaret
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = StatusCodes.Status401Unauthorized,
                title = "Unauthorized",
                traceId = context.TraceIdentifier,
                detail = "Access to this resource is denied due to invalid credentials."
            };

            // Returnera JSON som svar
            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentLength = jsonResponse.Length; // Uppdatera content-length
            return context.Response.WriteAsync(jsonResponse);
            //return context.Response.WriteAsJsonAsync(response);
        }

        private Task HandleForbiddenAsync(HttpContext context)
        {
            // Anpassa meddelandet för 403-svaret
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = StatusCodes.Status403Forbidden,
                title = "Forbidden",
                traceId = context.TraceIdentifier,
                detail = "Access to this resource is denied due to invalid credentials."
            };

            // Returnera JSON som svar
            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentLength = jsonResponse.Length; // Uppdatera content-length
            return context.Response.WriteAsync(jsonResponse);
            //return context.Response.WriteAsJsonAsync(response);
        }
    }
}
