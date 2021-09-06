using FluentValidation;
using OrderService.API.Application.Commands.DeliveryCompanyCommands;
using OrderService.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.DeliveryCompany
{
    public class DeleteDeliveryCompanyValidator : AbstractValidator<DeleteDeliveryCompanyCommand>
    {
        private IDeliveryCompanyService _deliveryCompanyService;

        public DeleteDeliveryCompanyValidator(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => "Entity must exists");
        }

        private async Task<bool> Exist(int id, CancellationToken cancellationToken) =>
            await _deliveryCompanyService.ExistsAsync(id, cancellationToken);
    }
}
