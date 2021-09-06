using FluentValidation;
using OrderService.API.Application.Queries.DeliveryCompanyQueries;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Validation.DeliveryCompany
{
    public class GetDeliveryCompanyValidator : AbstractValidator<GetDeliveryCompanyQuery>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public GetDeliveryCompanyValidator(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(query => string.Format(Resources.Resources.DeliveryCompanyNotFound, nameof(query.Id)));
        }

        private async Task<bool> Exist(int id, CancellationToken cancellationToken) =>
            await _deliveryCompanyService.ExistsAsync(id, cancellationToken);
    }
}
