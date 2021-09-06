using MediatR;

namespace OrderService.API.Application.Commands.Abstractions
{
    public abstract class BaseCommand<TEntity, TResponse> : IRequest<TResponse>
        where TEntity : class
    {
        public TEntity Entity { get; set; }
        public int? Id { get; set; }

        protected BaseCommand(int id, TEntity entity)
        {
            Id = id;
            Entity = entity;
        }

        protected BaseCommand(TEntity entity)
        {
            Entity = entity;
        }
    }
}
