using MediatR;
using OrderService.API.Contracts.Outgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.DeliveryCompanyQueries
{
    public class GetAllDeliveryCompanyQuery : IRequest<IReadOnlyCollection<DeliveryCompanyOutgoingDTO>>
    {
        public GetAllDeliveryCompanyQuery()
        {
        }
    }

    class GetAllDeliveryCompaniesQueryHandler : IRequestHandler<GetAllDeliveryCompanyQuery, IReadOnlyCollection<DeliveryCompanyOutgoingDTO>>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetAllDeliveryCompaniesQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<IReadOnlyCollection<DeliveryCompanyOutgoingDTO>> Handle(GetAllDeliveryCompanyQuery request,
            CancellationToken cancellationToken)
        {
            var deliveryCompanies = await _deliveryCompanyService.GetAllAsync(cancellationToken);

            return deliveryCompanies.Select(MapToFoundDeliveryCompanyDTO).ToArray();
        }

        private DeliveryCompanyOutgoingDTO MapToFoundDeliveryCompanyDTO(DeliveryCompany deliveryCompany)
        {
            return new DeliveryCompanyOutgoingDTO
            {
                Id = deliveryCompany.Id,
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }
    }
}
