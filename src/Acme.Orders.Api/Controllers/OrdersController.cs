using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.Orders.Api.Responses;
using Acme.Orders.Application.Commands;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Queries;
using Acme.Orders.Common.Enums;
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

        [HttpGet]
        [Route("", Name = "GetOrders")]
        public async Task<ActionResult<OrdersResponse>> GetOrders([FromQuery] ulong? cursor, [FromQuery] OrderStatus? status)
        {
            var orders = await _mediator.Send(new GetOrders(cursor, status));
            var response = orders.MapToResponseModel();

            if (!orders.IsLastPage && orders.Orders.Any())
                response.NextPage = Url.Link(nameof(GetOrders), new {cursor = orders.Orders.Last().Id, status = status});
          
            return Ok(response);
        }

        [HttpGet]
        [Route("summary")]
        public async Task<ActionResult<OrdersSummary>> GetOrderSummary()
        {
            var response = await _mediator.Send(new GetOrdersSummary());

            return Ok(response);
        }


        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateOrder()
        {
            var order = await _mediator.Send(new CreateOrderCommand());

            return CreatedAtAction(nameof(GetOrder), new { orderId = order }, order.ToString());
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrder(ulong orderId)
        {
            var order = await _mediator.Send(new GetOrder(orderId));

            return Ok(order);
        }

        [HttpGet]
        [Route("{orderId}/items")]
        public async Task<IActionResult> GetOrderItems(ulong orderId)
        {
            var orderItems = await _mediator.Send(new GetOrderItems(orderId));

            return Ok(orderItems);
        }

        [HttpPost]
        [Route("{orderId}/items/add")]
        public async Task<IActionResult> AddOrderItem(ulong orderId, [FromBody] OrderItemDto orderItem)
        {
            var order = await _mediator.Send(new AddOrderItemCommand(orderId, orderItem));

            return AcceptedAtAction(nameof(GetOrderItems), new { orderId = order }, null);
        }

        [HttpDelete]
        [Route("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem(ulong orderId, int itemId)
        {
            var order = await _mediator.Send(new DeleteOrderItemCommand(itemId, orderId));

            return NoContent();
        }
        
        [HttpPost]
        [Route("{orderId}/place")]
        public async Task<IActionResult> PlaceOrder(ulong orderId)
        {
            var order = await _mediator.Send(new PlaceOrderCommand(orderId));

            return AcceptedAtAction(nameof(GetOrder), new { orderId = orderId }, null);
        }
    }
}
