using OrderService.API.Application.Commands.DeliveryCompanyCommands;
using OrderService.API.Application.Validation.Abstractions;

namespace OrderService.API.Application.Validation.DeliveryCompany
{
    public class AddDeliveryCompanyValidator : DeliveryCompanyValidatorBase<AddDeliveryCompanyCommand, int>
    {
        public AddDeliveryCompanyValidator() : base() { }
    }
}
