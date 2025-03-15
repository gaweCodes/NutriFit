using MediatR;

namespace SharedKernel.Application;

public interface ICommand : IRequest { }
public interface ICommand<out TResponse> : IRequest<TResponse> { }