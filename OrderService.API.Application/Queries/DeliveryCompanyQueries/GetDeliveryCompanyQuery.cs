using MediatR;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.DeliveryCompanyQueries
{
    public class GetDeliveryCompanyQuery : IRequest<DeliveryCompanyIncomingDTO>
    {
        public int Id { get; set; }

        public GetDeliveryCompanyQuery(int id)
        {
            Id = id;
        }
    }

    class GetDeliveryCompanyQueryHandler : IRequestHandler<GetDeliveryCompanyQuery, DeliveryCompanyIncomingDTO>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetDeliveryCompanyQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<DeliveryCompanyIncomingDTO> Handle(GetDeliveryCompanyQuery request, CancellationToken cancellationToken)
        {
            var deliveryCompany = await _deliveryCompanyService.GetAsync(request.Id, cancellationToken);
            if (deliveryCompany == null)
                return null;

            return MapToDeliveryCompany(deliveryCompany);
        }

        private DeliveryCompanyIncomingDTO MapToDeliveryCompany(DeliveryCompany deliveryCompany)
        {
            return new DeliveryCompanyIncomingDTO()
            {
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }

    }
}
