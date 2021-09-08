using FluentValidation;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.Abstractions
{
    public class OrderValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BaseCommand<OrderDTO, TResponse>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public OrderValidatorBase(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Entity)));

            RuleFor(cmd => cmd.Entity.Status)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(cmd => Resources.Resources.OrderStatusRequired)
                .Must(HasValidStatus)
                .WithMessage(cmd => Resources.Resources.OrderStatusInvalid);

            RuleFor(cmd => cmd.Entity.Cost)
                .Must(NotBeLessThanNull)
                .WithMessage(cmd => Resources.Resources.OrderCostNotBeLessThanNull);

            RuleFor(cmd => cmd.Entity.DeliveryCompanyId)
                .MustAsync(ExistDeliveryCompany)
                .WithMessage(cmd => Resources.Resources.DeliveryCompanyNotFound);
        }

        private bool NotBeNullOrWhitespace(string value) =>
            !string.IsNullOrWhiteSpace(value);

        private bool HasValidStatus(string value) =>
            value == "Processed" || value == "On the way" || value == "Delivered";

        private bool NotBeLessThanNull(decimal value) =>
            value > 0;

        private async Task<bool> ExistDeliveryCompany(int id, CancellationToken cancellationToken) =>
            await _deliveryCompanyService.ExistsAsync(id, cancellationToken);
    }
}
