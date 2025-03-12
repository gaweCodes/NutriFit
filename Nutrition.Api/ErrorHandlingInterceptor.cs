using Grpc.Core;
using Grpc.Core.Interceptors;
using SharedKernel.Domain;
using SharedKernel.Infrastructure;

namespace Nutrition.Api;

public class ErrorHandlingInterceptor(ILogger<ErrorHandlingInterceptor> logger) : Interceptor
{
    public async override Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        await HandleErrorsAsync(() => continuation(request, responseStream, context));
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        return await HandleErrorsAsync(() => continuation(request, context));
    }

    private async Task<TResponse> HandleErrorsAsync<TResponse>(Func<Task<TResponse>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            throw MapException(ex);
        }
    }

    private async Task HandleErrorsAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            throw MapException(ex);
        }
    }

    private RpcException MapException(Exception ex)
    {
        return ex switch
        {
            ValidationRuleException v => new RpcException(new Status(StatusCode.InvalidArgument, v.Message)),
            BusinessRuleException b => new RpcException(new Status(StatusCode.FailedPrecondition, b.Message)),
            EntityNotFoundException n => new RpcException(new Status(StatusCode.NotFound, n.Message)),
            _ => LogAndCreateInternalException(ex)
        };
    }

    private RpcException LogAndCreateInternalException(Exception ex)
    {
        logger.LogError(ex, "Unhandled exception occurred.");
        return new RpcException(new Status(StatusCode.Internal, "Ein unerwarteter Fehler ist aufgetreten"));
    }
}
