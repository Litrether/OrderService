using FluentValidation;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;

namespace OrderService.API.Application.Validation.Abstractions
{
    public class DeliveryCompanyValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BaseCommand<DeliveryCompanyDTO, TResponse>
    {
        public DeliveryCompanyValidatorBase()
        {
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
                .NotNull()
                .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Entity)));

            RuleFor(cmd => cmd.Entity.Name)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.DeliveryCompanyNameRequired);

            RuleFor(cmd => cmd.Entity.Rating)
                .Must(NotBeLessThanNull)
                .WithMessage(Resources.Resources.DeliveryCompanyRatingNotBeLessThanNull);
        }

        private bool NotBeNullOrWhitespace(string value) =>
            !string.IsNullOrWhiteSpace(value);

        private bool NotBeLessThanNull(double value) =>
            value > 0;
    }
}
