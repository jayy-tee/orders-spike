using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Abstracts;
using Acme.Orders.Application.Common;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Model.Extensions;
using Acme.Orders.Application.Notifications;
using Acme.Orders.Domain.Entities;
using MediatR;

namespace Acme.Orders.Application.Commands
{
    public class CreateOrder : ICommandRequest<OrderDto>
    {
        public Guid CustomerId { get; private set; }

        public class CreateOrderHandler : IRequestHandler<CreateOrder, OrderDto>
        {
            private readonly IAcmeDbContext _context;
            private readonly IIdGenerator _idGenerator;
            private readonly IPublisher _publisher;

            public CreateOrderHandler(IAcmeDbContext context, IIdGenerator idGenerator, IPublisher publisher)
            {
                _context = context;
                _idGenerator = idGenerator;
                _publisher = publisher;
            }

            public async Task<OrderDto> Handle(CreateOrder command, CancellationToken cancellationToken)
            {
                var order = new Order(_idGenerator.GetId());
                await _context.Orders.AddAsync(order, cancellationToken);

                await _publisher.Publish(new OrderCreatedNotification
                {
                    OrderId = order.Id
                }, cancellationToken);

                return order.MapToDto();
            }
        } 
    }    
}
