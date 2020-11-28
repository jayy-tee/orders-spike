using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Abstracts;
using Acme.Orders.Application.Common;
using Acme.Orders.Domain.Entities;
using MediatR;

namespace Acme.Orders.Application.Commands
{
    public class CreateOrderCommand : IRequest<ulong>
    {
        public Guid CustomerId { get; private set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ulong>
        {
            private readonly IAcmeDbContext _context;
            private readonly IIdGenerator _idGenerator;

            public CreateOrderCommandHandler(IAcmeDbContext context, IIdGenerator idGenerator)
            {
                _context = context;
                _idGenerator = idGenerator;
            }

            public async Task<ulong> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                var order = new Order(_idGenerator.GetId());
                await _context.Orders.AddAsync(order, cancellationToken);
                await _context.SaveAsync(cancellationToken);

                return order.Id;
            }
        } 
    }    
}
