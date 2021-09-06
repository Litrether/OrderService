using OrderService.API.Application.Commands.OrderCommands;
using OrderService.API.Application.Validation.Abstractions;
using OrderService.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.API.Application.Validation.OrderCommands
{
    public class AddOrderValidator : OrderValidatorBase<AddOrderCommand, int>
    {
        public AddOrderValidator(IDeliveryCompanyService _deliveryCompanyService) : base(_deliveryCompanyService)
        { }
    }
}
