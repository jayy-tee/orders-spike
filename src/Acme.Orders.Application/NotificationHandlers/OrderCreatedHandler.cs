using System.Threading;
using System.Threading.Tasks;
using Acme.Orders.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Application.NotificationHandlers
{
    public class OrderCreatedHandler : INotificationHandler<OrderCreatedNotification>
    {
        private readonly ILogger<OrderCreatedHandler> _logger;

        public OrderCreatedHandler(ILogger<OrderCreatedHandler> logger)
        {
            _logger = logger;
        }
        
        public Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order created with ID: {notification.OrderId}");
            
            return Task.CompletedTask;
        }
    }
}