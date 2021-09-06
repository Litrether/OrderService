using FluentValidation;
using OrderService.API.Application.Commands.DeliveryCompanyCommands;
using OrderService.API.Application.Validation.Abstractions;
using OrderService.API.Contracts.Outgoing.Abstractions;
using OrderService.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
            RuleFor(query => query.Id)
               .NotNull()
               .WithMessage(query => "Value required");

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(query => "Value required");
        }

        private async Task<bool> Exist(int? id, CancellationToken cancellationToken) =>
            id.HasValue && await _deliveryCompanyService.ExistsAsync(id.Value, cancellationToken);
    }
}
