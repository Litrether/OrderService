using FluentValidation;
using OrderService.API.Application.Commands.DeliveryCompanyCommands;
using OrderService.API.Application.Validation.Abstractions;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.DeliveryCompany
{
    public class UpdateDeliveryCompanyValidator : DeliveryCompanyValidatorBase<UpdateDeliveryCompanyCommand, Response>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public UpdateDeliveryCompanyValidator(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.DeliveryCompanyNotFound, cmd.Id));
        }

        private async Task<bool> Exist(int? id, CancellationToken cancellationToken) =>
            id.HasValue && await _deliveryCompanyService.ExistsAsync(id.Value, cancellationToken);
    }
}
