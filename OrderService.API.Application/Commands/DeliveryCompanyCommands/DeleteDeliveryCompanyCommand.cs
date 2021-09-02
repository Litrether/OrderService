using MediatR;
using OrderService.Data.Services;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.API.Application.Commands.DeliveryCompanyCommands
{
    public class DeleteDeliveryCompanyCommand : IRequest
    {
        public int Id { get; }

        public DeleteDeliveryCompanyCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteDeliveryCompanyCommandHandler : IRequestHandler<DeleteDeliveryCompanyCommand>
    {
        private readonly IDeliveryCompanyService _deliveryCompanyService;

        public DeleteDeliveryCompanyCommandHandler(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task<Unit> Handle(DeleteDeliveryCompanyCommand request, CancellationToken cancellationToken)
        {
            await _deliveryCompanyService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
