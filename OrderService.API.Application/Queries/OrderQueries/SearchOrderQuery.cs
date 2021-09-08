using MediatR;
using OrderService.API.Application.Abstractions;
using OrderService.API.Contracts;
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
        private readonly IOrderService _orderService;

        public SearchOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<PagedResponse<FoundOrderDTO>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
        {
            OrderSearchCondition searchCondition = new OrderSearchCondition()
            {
                Status = GetFilterValues(request.SearchCondition.Status),
                Cost = request.SearchCondition.Cost,
                DeliveryCompanyId = request.SearchCondition.DeliveryCompanyId,
                ProductId = request.SearchCondition.ProductId,
                UserId = GetFilterValues(request.SearchCondition.UserId),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty,
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Order> foundDeliveryCompany = await _orderService.FindAsync(
                searchCondition, sortProperty);
            FoundOrderDTO[] mappedDeliveryCompany = foundDeliveryCompany.Select(MapToFoundOrderDTO).ToArray();
            var totalCount = await _orderService.CountAsync(searchCondition);

            return new PagedResponse<FoundOrderDTO>
            {
                Items = mappedDeliveryCompany,
                TotalCount = totalCount,
            };
        }

        private FoundOrderDTO MapToFoundOrderDTO(Order order)
        {
            return new FoundOrderDTO
            {
                Id = order.Id,
                Status = order.Status,
                Cost = order.Cost,
                UserId = order.UserId,
                ProductId = order.ProductId,
                DeliveryCompanyId = order.DeliveryCompanyId,
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
