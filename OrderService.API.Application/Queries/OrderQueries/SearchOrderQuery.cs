using MediatR;
using OrderService.API.Application.Abstractions;
using OrderService.API.Contracts;
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

namespace OrderService.API.Application.Queries.OrderQueries
{
    public class SearchOrderQuery : PagedSearchQuery<FoundOrderDTO, OrderSearchCondition>
    {
        public SearchOrderQuery(OrderSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    class SearchOrderQueryHandler : IRequestHandler<SearchOrderQuery, PagedResponse<FoundOrderDTO>>
    {
        private readonly IOrderViewService _orderViewService;

        public SearchOrderQueryHandler(IOrderViewService orderViewService)
        {
            _orderViewService = orderViewService;
        }

        public async Task<PagedResponse<FoundOrderDTO>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
        {
            OrderSearchCondition searchCondition = new OrderSearchCondition()
            {
                Status = GetFilterValues(request.SearchCondition.Status),
                Cost = request.SearchCondition.Cost,
                DeliveryCompany = GetFilterValues(request.SearchCondition.DeliveryCompany),
                Product = GetFilterValues(request.SearchCondition.Product),
                Username = GetFilterValues(request.SearchCondition.Username),
                DeliveredAt = request.SearchCondition.DeliveredAt,
                OrderedAt = request.SearchCondition.OrderedAt,
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty,
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<OrderView> foundDeliveryCompany = await _orderViewService.FindAsync(
                searchCondition, sortProperty);
            FoundOrderDTO[] mappedDeliveryCompany = foundDeliveryCompany.Select(MapToFoundDeliveryCompanyDTO).ToArray();
            var totalCount = await _orderViewService.CountAsync(searchCondition);

            return new PagedResponse<FoundOrderDTO>
            {
                Items = mappedDeliveryCompany,
                TotalCount = totalCount,
            };
        }

        private FoundOrderDTO MapToFoundDeliveryCompanyDTO(OrderView deliveryCompany)
        {
            return new FoundOrderDTO
            {
                Id = deliveryCompany.Id,
                Status = deliveryCompany.Status,
                Cost = deliveryCompany.Cost,
                Username = deliveryCompany.Username,
                Product = deliveryCompany.Product,
                DeliveryCompany = deliveryCompany.DeliveryCompany,
                OrderedAt = deliveryCompany.OrderedAt,
                DeliveredAt = deliveryCompany.DeliveredAt,
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
