using MediatR;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.OrderCommands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; }

        public DeleteOrderCommand(int id)
        {
            Id = id;
        }
    }

    class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderService _orderService;

        public DeleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}