using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Commands
{
    public class DeleteOrderItem : ICommandRequest
    {
        public ulong OrderId { get; private set; }
        public int ItemId { get; private set; }

        public DeleteOrderItem(int itemId, ulong orderId)
        {
            OrderId = orderId;
            ItemId = itemId;
        }

        public class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItem>
        {
            private readonly IAcmeDbContext _context;

            public DeleteOrderItemHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteOrderItem command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);
                _ = order != null ? true : throw new NotFoundException("Order Not Found");
            
                order.RemoveItem(order.Items.Where(i => i.Id == command.ItemId).Single());
            
                return Unit.Value;
            }
        } 
    }    
}
