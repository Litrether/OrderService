using MediatR;
using OrderService.API.Contracts.Outgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.OrderQueries
{
    public class GetAllOrderQuery : IRequest<IReadOnlyCollection<FoundOrderDTO>>
    {
        public GetAllOrderQuery()
        {
        }
    }

    class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IReadOnlyCollection<FoundOrderDTO>>
    {
        private readonly IOrderViewService _orderService;

        public GetAllOrderQueryHandler(IOrderViewService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IReadOnlyCollection<FoundOrderDTO>> Handle(GetAllOrderQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(cancellationToken);

            return orders.Select(MapToFoundOrderDTO).ToArray();
        }


        private FoundOrderDTO MapToFoundOrderDTO(OrderView order)
        {
            return new FoundOrderDTO
            {
                Id = order.Id,
                Status = order.Status,
                Cost = order.Cost,
                Username = order.Username,
                Product = order.Product,
                DeliveryCompany = order.DeliveryCompany,
                DeliveredAt = order.DeliveredAt,
                OrderedAt = order.OrderedAt,
            };
        }
    }
}
