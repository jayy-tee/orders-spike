using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using Acme.Orders.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Queries
{
    public class GetOrder : IRequest<OrderDto>
    {
        public ulong OrderId { get; private set; }

        public GetOrder(ulong orderId)
        {
            OrderId = orderId;
        }

        public class GetOrderHandler : IRequestHandler<GetOrder, OrderDto>
        {
            private readonly IAcmeDbContext _context;
            public GetOrderHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<OrderDto> Handle(GetOrder query, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == query.OrderId)
                            ?? throw new NotFoundException("Order Not Found");

                return order.MapToDto();
            }
        } 
    }    
}
