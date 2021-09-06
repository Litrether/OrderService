using OrderService.API.Application.Commands.OrderCommands;
using OrderService.API.Application.Validation.Abstractions;
using OrderService.Data.Services;

namespace OrderService.API.Application.Validation.OrderCommands
{
    public class AddOrderValidator : OrderValidatorBase<AddOrderCommand, int>
    {
        public AddOrderValidator(IDeliveryCompanyService _deliveryCompanyService) : base(_deliveryCompanyService)
        { }
    }
}
