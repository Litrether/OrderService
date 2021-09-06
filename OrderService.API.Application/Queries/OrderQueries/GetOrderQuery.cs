using MediatR;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.OrderQueries
{
    public class GetOrderQuery : IRequest<OrderDTO>
    {
        public int Id { get; set; }

        public GetOrderQuery(int id)
        {
            Id = id;
        }
    }

    class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDTO>
    {
        private readonly IOrderService _orderService;

        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(request.Id, cancellationToken);
            if (order == null)
                return null;

            return MapToOrder(order);
        }

        private OrderDTO MapToOrder(Order order)
        {
            return new OrderDTO()
            {
                Status = order.Status,
                Cost = order.Cost,
                DeliveredAt = order.DeliveredAt,
                DeliveryCompanyId = order.DeliveryCompanyId,
                OrderedAt = order.OrderedAt,
                ProductId = order.ProductId,
                UserId = order.UserId,
            };
        }

    }
}
