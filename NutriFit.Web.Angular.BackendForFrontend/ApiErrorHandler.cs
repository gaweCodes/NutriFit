using System.Net;

namespace NutriFit.Web.Angular.BackendForFrontend;

public class ApiErrorHandler(ILogger<ApiErrorHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            ApiError? apiError = null;
            try
            {
                var text = await response.Content.ReadAsStringAsync();

                apiError = await response.Content.ReadFromJsonAsync<ApiError>(cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Failed to parse ApiError response.");
            }

            throw new HttpRequestException(apiError?.Message ?? "Ein unerwarteter Fehler ist aufgetreten.", null, apiError == null ? HttpStatusCode.InternalServerError : response.StatusCode);
        }
        
        return response;
    }
}
