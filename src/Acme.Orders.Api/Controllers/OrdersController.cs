using System;
using System.Threading.Tasks;
using Acme.Orders.Application.Commands;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateOrder()
        {
            var order = await _mediator.Send(new CreateOrderCommand());

            return CreatedAtAction(nameof(GetOrder), new { orderId = order }, order);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var order = await _mediator.Send(new GetOrderQuery(orderId));

            return Ok(order);
        }

        [HttpGet]
        [Route("{orderId}/items")]
        public async Task<IActionResult> GetOrderItems(Guid orderId)
        {
            var orderItems = await _mediator.Send(new GetOrderItemsQuery(orderId));

            return Ok(orderItems);
        }

        [HttpPost]
        [Route("{orderId}/items/add")]
        public async Task<IActionResult> AddOrderItem(Guid orderId, [FromBody] OrderItemDto orderItem)
        {
            var order = await _mediator.Send(new AddOrderItemCommand(orderId, orderItem));

            return AcceptedAtAction(nameof(GetOrderItems), new { orderId = order }, null);
        }

        [HttpDelete]
        [Route("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, int itemId)
        {
            var order = await _mediator.Send(new DeleteOrderItemCommand(itemId, orderId));

            return NoContent();
        }
    }
}
