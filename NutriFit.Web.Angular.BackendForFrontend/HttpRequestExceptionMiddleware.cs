using OpenTelemetry.Trace;
using System.Net;

namespace NutriFit.Web.Angular.BackendForFrontend;

public class HttpRequestExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (HttpRequestException ex)
        {
            var errorResponse = new { message = ex.Message };
            context.Response.StatusCode = (int)(ex.StatusCode ?? HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
