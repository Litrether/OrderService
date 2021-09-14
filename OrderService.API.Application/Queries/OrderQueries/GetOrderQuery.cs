using MediatR;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Queries.OrderQueries
{
    public class GetOrderQuery : IRequest<OrderIncomingDTO>
    {
        public int Id { get; set; }

        public GetOrderQuery(int id)
        {
            Id = id;
        }
    }

    class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderIncomingDTO>
    {
        private readonly IOrderService _orderService;

        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderIncomingDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(request.Id, cancellationToken);
            if (order == null)
                return null;

            return MapToOrder(order);
        }

        private OrderIncomingDTO MapToOrder(Order order)
        {
            return new OrderIncomingDTO()
            {
                Status = order.Status,
                Cost = order.Cost,
                DeliveryCompanyId = order.DeliveryCompanyId,
                ProductId = order.ProductId,
                Username = order.Username,
            };
        }

    }
}
