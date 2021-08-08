using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Application.Behaviours
{
    public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommandRequest<TResponse>
    {
        private readonly ILogger<UnitOfWorkBehaviour<TRequest, TResponse>> _logger;
        private readonly IAcmeDbContext _acmeDbContext;

        public UnitOfWorkBehaviour(ILogger<UnitOfWorkBehaviour<TRequest, TResponse>> logger, IAcmeDbContext acmeDbContext)
        {
            _logger = logger;
            _acmeDbContext = acmeDbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            await _acmeDbContext.SaveAsync(cancellationToken);

            return response;
        }
    }
}