using MediatR;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.DeliveryCompanyCommands
{
    public class AddDeliveryCompanyCommand : BaseCommand<DeliveryCompanyIncomingDTO, int>
    {
        public AddDeliveryCompanyCommand(DeliveryCompanyIncomingDTO deliveryCompany) : base(deliveryCompany) { }
    }

    class AddDeliveryCompanyCommandHandler : IRequestHandler<AddDeliveryCompanyCommand, int>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyCommandService;

        public AddDeliveryCompanyCommandHandler(IDeliveryCompanyService deliveryCommandService)
        {
            _deliveryCompanyCommandService = deliveryCommandService;
        }

        public async Task<int> Handle(AddDeliveryCompanyCommand request, CancellationToken cancellationToken)
        {
            var deliveryCompany = MapToDeliveryCompany(request.Entity);
            var insertDeliveryCompany = await _deliveryCompanyCommandService.CreateAsync(deliveryCompany);
            return insertDeliveryCompany.Id;
        }

        private DeliveryCompany MapToDeliveryCompany(DeliveryCompanyIncomingDTO deliveryCompany)
        {
            return new DeliveryCompany
            {
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }
    }
}
