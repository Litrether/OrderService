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
               .WithMessage(cmd => "");

            RuleFor(cmd => cmd.Entity.Status)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(cmd => "")
                .Must(HasValidStatus)
                .WithMessage(cmd => "");

            RuleFor(cmd => cmd.Entity.Cost)
                .Must(NotBeLessThanNull)
                .WithMessage(cmd => "");

            RuleFor(cmd => cmd.Entity.OrderedAt)
                .NotNull()
                .WithMessage(cmd => "");

            RuleFor(cmd => cmd.Entity.DeliveryCompanyId)
                .MustAsync(ExistDeliveryCompany)
                .WithMessage(cmd => "");
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
