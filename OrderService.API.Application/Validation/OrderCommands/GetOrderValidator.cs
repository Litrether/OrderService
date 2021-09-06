using FluentValidation;
using OrderService.API.Application.Queries.OrderQueries;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.OrderCommands
{
    class GetOrderValidator : AbstractValidator<GetOrderQuery>
    {
        private readonly IOrderService _orderService;

        public GetOrderValidator(IOrderService orderService)
        {
            _orderService = orderService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => "asd");

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(query => "");
        }

        private async Task<bool> Exist(int id, CancellationToken cancellationToken) =>
             await _orderService.ExistsAsync(id, cancellationToken);
    }
}
