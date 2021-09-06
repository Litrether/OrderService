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
    public class SearchDeliveryCompanyQuery : PagedSearchQuery<FoundDeliveryCompanyDTO, DeliveryCompanySearchCondition>
    {
        public SearchDeliveryCompanyQuery(DeliveryCompanySearchCondition searchCondition) : base(searchCondition)
        { }
    }

    class SearchDeliveryCompanyQueryHandler : IRequestHandler<SearchDeliveryCompanyQuery, PagedResponse<FoundDeliveryCompanyDTO>>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public SearchDeliveryCompanyQueryHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<PagedResponse<FoundDeliveryCompanyDTO>> Handle(SearchDeliveryCompanyQuery request, CancellationToken cancellationToken)
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
            FoundDeliveryCompanyDTO[] mappedDeliveryCompany = foundDeliveryCompany.Select(MapToFoundDeliveryCompanyDTO).ToArray();
            var totalCount = await _deliveryCompanyService.CountAsync(searchCondition);

            return new PagedResponse<FoundDeliveryCompanyDTO>
            {
                Items = mappedDeliveryCompany,
                TotalCount = totalCount,
            };
        }

        private FoundDeliveryCompanyDTO MapToFoundDeliveryCompanyDTO(DeliveryCompany deliveryCompany)
        {
            return new FoundDeliveryCompanyDTO
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

            if (propertyName.Equals("deliveryCompanyName", StringComparison.InvariantCultureIgnoreCase))
                return nameof(DeliveryCompany.Name);

            if (propertyName.Equals("deliveryCompanyRating", StringComparison.InvariantCultureIgnoreCase))
                return nameof(DeliveryCompany.Rating);

            return propertyName;
        }
    }
}
