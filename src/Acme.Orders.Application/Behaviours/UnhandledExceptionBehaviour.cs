using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Domain.Exceptions;
using MediatR;

namespace Acme.Orders.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public UnhandledExceptionBehaviour()
        {
            //TODO: Add logger here for logging goodness.
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (OrdersDomainException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
        }
    }
}
