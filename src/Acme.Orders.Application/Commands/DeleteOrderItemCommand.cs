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
    public class DeleteOrderItemCommand : IRequest
    {
        public ulong OrderId { get; private set; }
        public int ItemId { get; private set; }

        public DeleteOrderItemCommand(int itemId, ulong orderId)
        {
            OrderId = orderId;
            ItemId = itemId;
        }

        public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
        {
            private readonly IAcmeDbContext _context;

            public DeleteOrderItemCommandHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);
                _ = order != null ? true : throw new NotFoundException("Order Not Found");
            
                order.RemoveItem(order.Items.Where(i => i.Id == command.ItemId).Single());
                await _context.SaveAsync(cancellationToken);
            
                return Unit.Value;
            }
        } 
    }    
}
