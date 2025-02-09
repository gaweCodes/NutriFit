using MediatR;

namespace SharedKernel.Application;

public interface IQuery<out TResult> : IRequest<TResult>
{
}