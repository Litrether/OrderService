using MediatR;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.DeliveryCompanyQueries
{
    public class GetDeliveryCompanyQuery : IRequest<DeliveryCompanyDTO>
    {
        public int Id { get; set; }

        public GetDeliveryCompanyQuery(int id)
        {
            Id = Id;
        }
    }

    class GetDeliveryCompanyQueryHandler : IRequestHandler<GetDeliveryCompanyQuery, DeliveryCompanyDTO>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetDeliveryCompanyQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<DeliveryCompanyDTO> Handle(GetDeliveryCompanyQuery request, CancellationToken cancellationToken)
        {
            var deliveryCompany = await _deliveryCompanyService.GetAsync(request.Id, cancellationToken);
            if (deliveryCompany == null)
                return null;

            return MapToDeliveryCompany(deliveryCompany);
        }

        private DeliveryCompanyDTO MapToDeliveryCompany(DeliveryCompany deliveryCompany)
        {
            return new DeliveryCompanyDTO()
            {
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }

    }
}
