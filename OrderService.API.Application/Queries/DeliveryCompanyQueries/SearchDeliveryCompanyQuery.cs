using MediatR;
using OrderService.API.Application.Abstractions;
using OrderService.API.Contracts.Incoming.SearchConditions;
using OrderService.API.Contracts.Outgoing;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.DeliveryCompanyQueries
{
    public class SearchDeliveryCompanyQuery : PagedSearchQuery<DeliveryCompanyOutgoingDTO, DeliveryCompanySearchCondition>
    {
        public SearchDeliveryCompanyQuery(DeliveryCompanySearchCondition searchCondition) : base(searchCondition)
        { }
    }

    class SearchDeliveryCompanyQueryHandler : IRequestHandler<SearchDeliveryCompanyQuery, PagedResponse<DeliveryCompanyOutgoingDTO>>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public SearchDeliveryCompanyQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<PagedResponse<DeliveryCompanyOutgoingDTO>> Handle(SearchDeliveryCompanyQuery request, CancellationToken cancellationToken)
        {
            DeliveryCompanySearchCondition searchCondition = new DeliveryCompanySearchCondition()
            {
                Name = GetFilterValues(request.SearchCondition.Name),
                Rating = request.SearchCondition.Rating,
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty,
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<DeliveryCompany> foundDeliveryCompany = await _deliveryCompanyService.FindAsync(
                searchCondition, sortProperty);
            DeliveryCompanyOutgoingDTO[] mappedDeliveryCompany = foundDeliveryCompany.Select(MapToFoundDeliveryCompanyDTO).ToArray();
            var totalCount = await _deliveryCompanyService.CountAsync(searchCondition);

            return new PagedResponse<DeliveryCompanyOutgoingDTO>
            {
                Items = mappedDeliveryCompany,
                TotalCount = totalCount,
            };
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

        private string[] GetFilterValues(ICollection<string> values)
        {
            return values == null
                ? Array.Empty<string>()
                : values.Select(v => v.Trim()).Distinct().ToArray();
        }

        protected string GetSortProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return nameof(DeliveryCompany.Id);

            if (propertyName.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                return nameof(DeliveryCompany.Name);

            if (propertyName.Equals("Rating", StringComparison.InvariantCultureIgnoreCase))
                return nameof(DeliveryCompany.Rating);

            return propertyName;
        }
    }
}
