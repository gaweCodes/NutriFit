using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NutriFit.BackendForFrontend;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is HttpRequestException httpEx && httpEx.StatusCode.HasValue)
        {
            context.Result = new ObjectResult(new { error = httpEx.Message })
            {
                StatusCode = (int)httpEx.StatusCode.Value
            };
            context.ExceptionHandled = true;
        }
    }
}
