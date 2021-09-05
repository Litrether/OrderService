using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.API.Contracts.Outgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;

namespace OrderService.API.Application.Queries.DeliveryCompanyQueries
{
    public class GetAllDeliveryCompanyQuery : IRequest<IReadOnlyCollection<FoundDeliveryCompanyDTO>>
    {
        public GetAllDeliveryCompanyQuery()
        {
        }
    }

    public class GetAllDeliveryCompaniesQueryHandler : IRequestHandler<GetAllDeliveryCompanyQuery, IReadOnlyCollection<FoundDeliveryCompanyDTO>>
        {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetAllDeliveryCompaniesQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<IReadOnlyCollection<FoundDeliveryCompanyDTO>> Handle(GetAllDeliveryCompanyQuery request,
            CancellationToken cancellationToken)
        {
            var deliveryCompanies = await _deliveryCompanyService.GetAllAsync(cancellationToken);

            return deliveryCompanies.Select(MapToFoundDeliveryCompaniesDTO).ToArray();
        }


        private FoundDeliveryCompanyDTO MapToFoundDeliveryCompaniesDTO(DeliveryCompany deliveryCompany)
        {
            return new FoundDeliveryCompanyDTO
            {
                Id = deliveryCompany.Id,
                Name = deliveryCompany.Name,
                Rating = deliveryCompany.Rating,
            };
        }
    }
}
