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
    public class SearchOrderQuery : PagedSearchQuery<OrderOutgoingDTO, OrderSearchCondition>
    {
        public SearchOrderQuery(OrderSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    class SearchOrderQueryHandler : IRequestHandler<SearchOrderQuery, PagedResponse<OrderOutgoingDTO>>
    {
        private readonly IOrderService _orderService;

        public SearchOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<PagedResponse<OrderOutgoingDTO>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
        {
            OrderSearchCondition searchCondition = new OrderSearchCondition()
            {
                Status = GetFilterValues(request.SearchCondition.Status),
                Cost = request.SearchCondition.Cost,
                DeliveryCompanyId = request.SearchCondition.DeliveryCompanyId,
                ProductId = request.SearchCondition.ProductId,
                Username = GetFilterValues(request.SearchCondition.Username),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty,
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Order> foundDeliveryCompany = await _orderService.FindAsync(
                searchCondition, sortProperty);
            OrderOutgoingDTO[] mappedDeliveryCompany = foundDeliveryCompany.Select(MapToFoundOrderDTO).ToArray();
            var totalCount = await _orderService.CountAsync(searchCondition);

            return new PagedResponse<OrderOutgoingDTO>
            {
                Items = mappedDeliveryCompany,
                TotalCount = totalCount,
            };
        }

        private OrderOutgoingDTO MapToFoundOrderDTO(Order order)
        {
            return new OrderOutgoingDTO
            {
                Id = order.Id,
                Status = order.Status,
                Cost = order.Cost,
                Username = order.Username,
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
                return nameof(Order.Id);

            if (propertyName.Equals("Status", StringComparison.InvariantCultureIgnoreCase))
                return nameof(Order.Status);

            if (propertyName.Equals("Username", StringComparison.InvariantCultureIgnoreCase))
                return nameof(Order.Username);

            if (propertyName.Equals("Cost", StringComparison.InvariantCultureIgnoreCase))
                return nameof(Order.Cost);

            if (propertyName.Equals("ProductId", StringComparison.InvariantCultureIgnoreCase))
                return nameof(Order.ProductId);

            if (propertyName.Equals("DeliveryCompanyId", StringComparison.InvariantCultureIgnoreCase))
                return nameof(Order.DeliveryCompanyId);

            return propertyName;
        }
    }
}
