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
    public class GetAllOrderQuery : IRequest<IReadOnlyCollection<OrderOutgoingDTO>>
    {
        public GetAllOrderQuery()
        {
        }
    }

    class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IReadOnlyCollection<OrderOutgoingDTO>>
    {
        private readonly IOrderService _orderService;
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetAllOrderQueryHandler(IOrderService orderService,
            IDeliveryCompanyService deliveryCompanyService)
        {
            _orderService = orderService;
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<IReadOnlyCollection<OrderOutgoingDTO>> Handle(GetAllOrderQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync();

            return orders.Select(MapToFoundOrderDTO).ToArray();
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
                //todo random delivery company
                DeliveryCompanyId = order.DeliveryCompanyId,
            };
        }
    }
}
