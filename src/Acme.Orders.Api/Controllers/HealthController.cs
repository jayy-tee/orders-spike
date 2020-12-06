using System;
using System.Threading.Tasks;
using Acme.Orders.Application.Exceptions;
using Acme.Orders.Application.Model;
using Acme.Orders.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acme.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;
        private readonly IMediator _mediator;

        public HealthController(ILogger<HealthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("live")]
        public IActionResult CheckLiveness()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ready")]
        public async Task<IActionResult> CheckReadyForTraffic()
        {
            try
            {
                var order = await _mediator.Send(new GetOrder(new ulong()));
            }
            catch (NotFoundException)
            {
                // Swallow the exception for negative test case.
            }

            return Ok();
        }
    }
}
