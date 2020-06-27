using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Queries
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; private set; }

        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
        {
            private readonly IAcmeDbContext _context;
            public GetOrderQueryHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == query.OrderId);
                return order.MapToDto();
            }
        } 
    }    
}
