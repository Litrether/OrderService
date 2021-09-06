using MediatR;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.DeliveryCompanyCommands
{
    public class UpdateDeliveryCompanyCommand : BaseCommand<DeliveryCompanyDTO, Response>
    {
        public UpdateDeliveryCompanyCommand(int id, DeliveryCompanyDTO deliveryCompany) : base(id, deliveryCompany) { }
    }

    class UpdateDeliveryCompanyCommandHandler : IRequestHandler<UpdateDeliveryCompanyCommand, Response>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public UpdateDeliveryCompanyCommandHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<Response> Handle(UpdateDeliveryCompanyCommand request, CancellationToken cancellationToken)
        {
            var deliveryCompany = await _deliveryCompanyService.GetAsync(request.Id, cancellationToken);
            if (deliveryCompany == null)
                return Response.Error;

            var deliveryCompanyToUpdate = MapDTOToDeliveryCompany(request.Entity, deliveryCompany);

            var updatedDeliveryCompany = await _deliveryCompanyService.UpdateAsync(deliveryCompanyToUpdate);
            if (updatedDeliveryCompany == null)
                return Response.Error;

            return Response.Successfull;
        }

        private DeliveryCompany MapDTOToDeliveryCompany(DeliveryCompanyDTO deliveryCompanyDTO, DeliveryCompany deliveryCompany)
        {
            deliveryCompany.Name = deliveryCompanyDTO.Name;
            deliveryCompany.Rating = deliveryCompanyDTO.Rating;
            return deliveryCompany;
        }
    }
}
