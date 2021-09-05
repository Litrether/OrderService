using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Application.Commands.DeliveryCompanyCommands;
using OrderService.API.Application.Queries.DeliveryCompanyQueries;
using OrderService.API.Contracts.Incoming.SearchConditions;
using OrderService.API.Contracts.IncomingOutgoing;

namespace OrderService.API.Controllers
{
    public class DeliveryCompanyController : MediatingControllerBase
    {
        public DeliveryCompanyController(IMediator _mediator) : base(_mediator)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryCompanies(CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new GetAllDeliveryCompanyQuery(), cancellationToken: cancellationToken);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryCompany([FromRoute] int id, CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new GetDeliveryCompanyQuery(id), cancellationToken: cancellationToken);

        [HttpPost("search")]
        public async Task<IActionResult> SearchDeliveryCompany([FromBody] DeliveryCompanySearchCondition searchCondition,
            CancellationToken cancellationToken = default) =>
            await ExecuteQueryAsync(new SearchDeliveryCompanyQuery(searchCondition), cancellationToken: cancellationToken);

        [HttpPost]
        public async Task<IActionResult> AddDeliveryCompany([FromBody] DeliveryCompanyDTO deliveryCompany,
            CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new AddDeliveryCompanyCommand(deliveryCompany), cancellationToken: cancellationToken);

        [HttpPut("{id")]
        public async Task<IActionResult> UpdateDeliveryCompany([FromRoute] int id, [FromBody] DeliveryCompanyDTO deliveryCompany,
            CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new UpdateDeliveryCompanyCommand(id, deliveryCompany), cancellationToken: cancellationToken);

        [HttpDelete("{id")]
        public async Task<IActionResult> DeleteDeliveryCompany([FromRoute] int id, CancellationToken cancellationToken = default) =>
            await ExecuteCommandAsync(new DeleteDeliveryCompanyCommand(id), cancellationToken: cancellationToken);






    }
}
