using CoffeeApplication.BusinessLogic.Service;
using CoffeeApplication.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeApplication.Api.Controllers
{
    [Route("api/v1/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        /// <summary>
        /// Returns a slimmed down collection of Orders that are linked to the user
        /// </summary>
        [HttpGet("GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var result = await _orderService.GetOrdersAsync(Id, cancellationToken: cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Adds Orders.        
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPost("PostOrder")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(Order order)
        {
            if (order is null)
                return BadRequest("A order must be present");

            await _orderService.AddOrderAsync(order);

            return Ok();
        }
    }
}
