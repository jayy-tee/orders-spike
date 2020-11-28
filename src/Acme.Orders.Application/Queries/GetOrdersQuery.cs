using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Queries
{
    public class GetOrdersQuery : IRequest<OrdersResult>
    {
        public int ResultsPerPage = 10;
        public ulong? Cursor;
        public OrderStatus? Status;

        public GetOrdersQuery(ulong? cursor, OrderStatus? status)
        {
            Cursor = cursor;
            Status = status;
        }

        public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrdersResult>
        {
            private readonly IAcmeDbContext _context;
            public GetOrdersQueryHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<OrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders.AsQueryable()
                    .Where(o => !query.Cursor.HasValue || o.Id > query.Cursor.Value)
                    .Where(o => !query.Status.HasValue|| o.Status == query.Status.Value)
                    .Take(query.ResultsPerPage+1)
                    .ToListAsync(cancellationToken);

                return new OrdersResult
                {
                    Orders = orders.Take(query.ResultsPerPage).Select(o => o.MapToDto()),
                    IsLastPage = orders.Count <= query.ResultsPerPage
                };
            }
        } 
    }    
}
