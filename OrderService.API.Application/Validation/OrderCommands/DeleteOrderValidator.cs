using FluentValidation;
using OrderService.API.Application.Commands.OrderCommands;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.OrderCommands
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        private IOrderService _orderService;

        public DeleteOrderValidator(IOrderService orderService)
        {
            _orderService = orderService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.DeliveryCompanyNotFound, cmd.Id));
        }

        private async Task<bool> Exist(int id, CancellationToken cancellationToken) =>
            await _orderService.ExistsAsync(id, cancellationToken);
    }
}
