using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Commands
{
    public class AddOrderItemCommand : IRequest
    {
        public Guid OrderId { get; private set; }
        public OrderItemDto OrderItem { get; private set; }


        public AddOrderItemCommand(Guid forOrder, OrderItemDto withItem)
        {
            OrderId = forOrder;
            OrderItem = withItem;
        }

        public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand>
        {
            private readonly IAcmeDbContext _context;
            public AddOrderItemCommandHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == command.OrderId, cancellationToken);
            
                order.AddItem(command.OrderItem.ToDomainModel());
                _context.Orders.Update(order);
                await _context.SaveAsync(cancellationToken);
            
                return Unit.Value;
            }
        } 
    }    
}
