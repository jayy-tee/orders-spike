using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Common.Enums;
using Acme.Orders.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acme.Orders.Application.Queries
{
    public class GetOrdersSummary : IRequest<OrdersSummary>
    {

        public GetOrdersSummary()
        {
        }

        public class GetOrdersSummaryHandler : IRequestHandler<GetOrdersSummary, OrdersSummary>
        {
            private readonly IAcmeDbContext _context;
            public GetOrdersSummaryHandler(IAcmeDbContext context)
            {
                _context = context;
            }

            public async Task<OrdersSummary> Handle(GetOrdersSummary query, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders
                    .GroupBy(o => o.Status)
                    .Select(g => new {Status = g.Key.ToString(), Count = g.Count()})
                    .ToDictionaryAsync(s => s.Status, c => c.Count, cancellationToken);
                
                return new OrdersSummary{ OrderStatuses = orders };
            }
        } 
    }    
}
