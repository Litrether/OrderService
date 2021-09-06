using OrderService.Data.Domain.Models;
using OrderService.API.Application.Commands.Abstractions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using OrderService.Data.Services;
using OrderService.API.Contracts.IncomingOutgoing;

namespace OrderService.API.Application.Commands.DeliveryCompanyCommands
{
    public class AddDeliveryCompanyCommand : BaseCommand<DeliveryCompanyDTO, int>
    {
        public AddDeliveryCompanyCommand(DeliveryCompanyDTO deliveryCompany) : base(deliveryCompany) { }
    }

    class AddDeliveryCompanyCommandHandler : IRequestHandler<AddDeliveryCompanyCommand, int>
    {
        private readonly IDeliveryCompanyService _deliveryCommandService;

        public AddDeliveryCompanyCommandHandler(IDeliveryCompanyService deliveryCommandService)
        {
            _deliveryCommandService = deliveryCommandService;
        }

        public async Task<int> Handle(AddDeliveryCompanyCommand request, CancellationToken cancellationToken)
        {
            var deliveryCompany = MapToDeliveryCompany(request.Entity);
            var insertDeliveryCompany = await _deliveryCommandService.CreateAsync(deliveryCompany);
            return insertDeliveryCompany.Id;
        }

        private DeliveryCompany MapToDeliveryCompany(DeliveryCompanyDTO deliveryCompany)
        {
            return new DeliveryCompany
            {
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }
    }
}
