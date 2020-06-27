using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Domain.Entities;
using MediatR;

namespace Acme.Orders.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; private set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
        {
            private readonly IAcmeDbContext _context;

            public CreateOrderCommandHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                var order = new Order();
                await _context.Orders.AddAsync(order, cancellationToken);
                await _context.SaveAsync(cancellationToken);

                return order.Id;
            }
        } 
    }    
}
