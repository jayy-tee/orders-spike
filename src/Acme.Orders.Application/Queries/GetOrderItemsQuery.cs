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
    public class GetOrderItemsQuery : IRequest<IEnumerable<OrderItemDto>>
    {
        public ulong OrderId { get; private set; }

        public GetOrderItemsQuery(ulong orderId)
        {
            OrderId = orderId;
        }

        public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, IEnumerable<OrderItemDto>>
        {
            private readonly IAcmeDbContext _context;

            public GetOrderItemsQueryHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<OrderItemDto>> Handle(GetOrderItemsQuery query, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.Id == query.OrderId)
                     ?? throw new NotFoundException("Order Not Found");

                var orderItems = order.Items.Select(i => i.MapToDto());

                return orderItems;
            }
        }
    }
}
