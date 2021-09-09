using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Application.Queries.OrderQueries;
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
        public async Task<IActionResult> GetOrder(int id, CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new GetOrderQuery(id), cancellationToken: cancellationToken);

        [HttpPost("search")]
        public async Task<IActionResult> SearchOrder([FromBody] Contracts.OrderSearchCondition searchCondition,
            CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new SearchOrderQuery(searchCondition), cancellationToken: cancellationToken);
    }
}
