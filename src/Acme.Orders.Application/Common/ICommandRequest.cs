using MediatR;

namespace Acme.Orders.Application.Common
{
    public interface ICommandRequest : ICommandRequest<Unit> { }
    public interface ICommandRequest<out TResponse> : IRequest<TResponse> { }
}