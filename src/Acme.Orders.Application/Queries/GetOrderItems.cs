using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Queries
{
    public class GetOrderItems : IRequest<ICollection<OrderItemDto>>
    {
        public ulong OrderId { get; private set; }

        public GetOrderItems(ulong orderId)
        {
            OrderId = orderId;
        }

        public class GetOrderItemsHandler : IRequestHandler<GetOrderItems, ICollection<OrderItemDto>>
        {
            private readonly IAcmeDbContext _context;

            public GetOrderItemsHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<ICollection<OrderItemDto>> Handle(GetOrderItems query, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == query.OrderId)
                     ?? throw new NotFoundException("Order Not Found");

                var orderItems = order.Items.Select(i => i.MapToDto()).ToList();

                return orderItems;
            }
        }
    }
}
