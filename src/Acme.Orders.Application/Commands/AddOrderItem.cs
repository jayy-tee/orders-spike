using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Commands
{
    public class AddOrderItem : IRequest
    {
        public ulong OrderId { get; private set; }
        public OrderItemDto OrderItem { get; private set; }


        public AddOrderItem(ulong forOrder, OrderItemDto withItem)
        {
            OrderId = forOrder;
            OrderItem = withItem;
        }

        public class AddOrderItemHandler : IRequestHandler<AddOrderItem>
        {
            private readonly IAcmeDbContext _context;
            public AddOrderItemHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddOrderItem command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);
                _ = order != null ? true : throw new NotFoundException("Order Not Found");
            
                order.AddItem(command.OrderItem.ToDomainModel());
                _context.Orders.Update(order);
                await _context.SaveAsync(cancellationToken);
            
                return Unit.Value;
            }
        } 
    }    
}
