using FluentValidation;
using OrderService.API.Application.Commands.Abstractions;
using OrderService.API.Contracts.IncomingOutgoing;

namespace OrderService.API.Application.Validation.Abstractions
{
    public class DeliveryCompanyValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BaseCommand<TResponse, DeliveryCompanyDTO>
    {
        public DeliveryCompanyValidatorBase()
        {
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
                .NotNull()
                .WithMessage(cmd => string.Format("Value required"));

            RuleFor(cmd => cmd.Entity.Name)
                .Must(NotBeNullOrWhitespace)
                .WithMessage("Delivery company name is requred");

            RuleFor(cmd => cmd.Entity.Rating)
                .Must(NotBeNullOrWhitespace)
                .WithMessage("Delivery company name is requred");
        }

        private bool NotBeNullOrWhitespace(string value) =>
            !string.IsNullOrWhiteSpace(value);

        private bool NotBeLessThanNull(int value) =>
            value > 0;
    }
}
