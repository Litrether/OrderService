using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.API.Application.Validation.Abstractions
{
    public class DeliveryCompanyValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
    {
    }
}
