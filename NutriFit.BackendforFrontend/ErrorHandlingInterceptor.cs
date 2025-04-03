using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Net;

namespace NutriFit.BackendForFrontend;

public class ErrorHandlingInterceptor : Interceptor
{
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);
        return new AsyncUnaryCall<TResponse>(
            HandleResponse(call.ResponseAsync),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);
        return new AsyncServerStreamingCall<TResponse>(
            new WrappedAsyncStreamReader<TResponse>(call.ResponseStream),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    private static async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> responseTask)
    {
        try
        {
            return await responseTask;
        }
        catch (RpcException ex)
        {
            throw new HttpRequestException(ex.Status.Detail, ex, MapGrpcToHttpStatus(ex.Status.StatusCode));
        }
    }

    private static HttpStatusCode MapGrpcToHttpStatus(StatusCode statusCode) => statusCode switch
    {
        StatusCode.InvalidArgument => HttpStatusCode.BadRequest,
        StatusCode.FailedPrecondition => HttpStatusCode.PreconditionFailed,
        StatusCode.NotFound => HttpStatusCode.NotFound,
        _ => HttpStatusCode.InternalServerError
    };

    private class WrappedAsyncStreamReader<T>(IAsyncStreamReader<T> inner) : IAsyncStreamReader<T>
    {
        public T Current => inner.Current;
        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            try
            {
                return await inner.MoveNext(cancellationToken);
            }
            catch (RpcException ex)
            {
                throw new HttpRequestException(ex.Status.Detail, ex, MapGrpcToHttpStatus(ex.Status.StatusCode));
            }
        }
    }
}
