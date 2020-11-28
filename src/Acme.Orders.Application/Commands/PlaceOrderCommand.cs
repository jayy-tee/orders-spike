using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Domain.Entities;
using Acme.Orders.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Commands
{
    public class PlaceOrderCommand : IRequest
    {
        public ulong OrderId { get; private set; }

        public PlaceOrderCommand(ulong orderId)
        {
            OrderId = orderId;
        }

        public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand>
        {
            private readonly IAcmeDbContext _context;

            public PlaceOrderCommandHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken)
                    ?? throw new NotFoundException("Order Not Found") ;
                
                order.Place();
                await _context.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        } 
    }    
}
