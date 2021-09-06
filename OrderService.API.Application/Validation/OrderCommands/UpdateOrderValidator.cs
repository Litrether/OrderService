using FluentValidation;
using OrderService.API.Application.Commands.OrderCommands;
using OrderService.API.Application.Validation.Abstractions;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.OrderCommands
{
    public class UpdateOrderValidator : OrderValidatorBase<UpdateOrderCommand, Response>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderValidator(IOrderService orderService,
            IDeliveryCompanyService deliveryCompanyService) : base(deliveryCompanyService)
        {
            _orderService = orderService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .NotNull()
                .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.OrderNotFound, nameof(cmd.Id)));
        }

        private async Task<bool> Exist(int? id, CancellationToken cancellationToken) =>
            id.HasValue && await _orderService.ExistsAsync(id.Value, cancellationToken);
    }
}
