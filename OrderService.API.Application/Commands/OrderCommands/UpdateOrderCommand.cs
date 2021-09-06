using MediatR;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Domain.Models;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.OrderCommands
{
    public class UpdateOrderCommand : BaseCommand<OrderDTO, Response>
    {
        public UpdateOrderCommand(int id, OrderDTO order) : base(id, order) { }
    }

    class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Response> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(request.Id, cancellationToken);
            if (order == null)
                return Response.Error;

            var deliveryCompanyToUpdate = MapDTOToOrder(request.Entity, order);

            var updatedOrder = await _orderService.UpdateAsync(deliveryCompanyToUpdate);
            if (updatedOrder == null)
                return Response.Error;

            return Response.Successfull;
        }

        private Order MapDTOToOrder(OrderDTO orderDTO, Order order)
        {
            order.Status = orderDTO.Status;
            order.Cost = orderDTO.Cost;
            order.DeliveredAt = orderDTO.DeliveredAt;
            order.OrderedAt = orderDTO.OrderedAt;
            order.UserId = orderDTO.UserId;
            order.ProductId = orderDTO.ProductId;
            order.DeliveryCompanyId = order.DeliveryCompanyId;
            return order;
        }
    }
}
