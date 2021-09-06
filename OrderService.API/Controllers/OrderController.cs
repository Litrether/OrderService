using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Application.Commands.OrderCommands;
using OrderService.API.Application.Queries.OrderQueries;
using OrderService.API.Contracts.IncomingOutgoing;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    public class OrderController : MediatingControllerBase
    {
        public OrderController(IMediator _mediator) : base(_mediator)
        { }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new GetAllOrderQuery(), cancellationToken: cancellationToken);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id, CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new GetOrderQuery(id), cancellationToken: cancellationToken);

        [HttpPost("search")]
        public async Task<IActionResult> SearchOrder([FromBody] Contracts.OrderSearchCondition searchCondition,
            CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new SearchOrderQuery(searchCondition), cancellationToken: cancellationToken);

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO order,
            CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new AddOrderCommand(order), cancellationToken: cancellationToken);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] OrderDTO order,
            CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new UpdateOrderCommand(id, order), cancellationToken: cancellationToken);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id, CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new DeleteOrderCommand(id), cancellationToken: cancellationToken);
    }
}
