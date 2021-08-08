using MediatR;

namespace Acme.Orders.Application.Notifications
{
    public class OrderCreatedNotification : INotification
    {
        public ulong OrderId { get; init; }
    }
}