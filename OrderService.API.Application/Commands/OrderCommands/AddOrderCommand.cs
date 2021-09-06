using MediatR;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.OrderCommands
{
    public class AddOrderCommand : BaseCommand<OrderDTO, int>
    {
        public AddOrderCommand(OrderDTO order) : base(order) { }
    }

    class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IOrderService _orderService;

        public AddOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = MapToOrder(request.Entity);
            var insertOrder = await _orderService.CreateAsync(order);
            return insertOrder.Id;
        }

        private Order MapToOrder(OrderDTO order)
        {
            return new Order
            {
                Status = order.Status,
                Cost = order.Cost,
                UserId = order.UserId,
                ProductId = order.ProductId,
                OrderedAt = order.OrderedAt,
                DeliveredAt = order.DeliveredAt,
                DeliveryCompanyId = order.DeliveryCompanyId,
            };
        }
    }
}
