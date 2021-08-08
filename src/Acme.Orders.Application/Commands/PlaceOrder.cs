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
    public class PlaceOrder : IRequest
    {
        public ulong OrderId { get; private set; }

        public PlaceOrder(ulong orderId)
        {
            OrderId = orderId;
        }

        public class PlaceOrderHandler : ICommandRequest<PlaceOrder>
        {
            private readonly IAcmeDbContext _context;

            public PlaceOrderHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(PlaceOrder command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken)
                    ?? throw new NotFoundException("Order Not Found") ;
                
                order.Place();

                return Unit.Value;
            }
        } 
    }    
}
