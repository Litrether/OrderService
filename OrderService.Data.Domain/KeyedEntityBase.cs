using MongoDB.Bson.Serialization.Attributes;
using OrderService.Data.Core;
using OrderService.Data.Domain.Models;

namespace OrderService.Data.Domain
{
    public abstract class KeyedEntityBase
    {
        [BsonId(IdGenerator = typeof(Int32IdGenerator<Order>))]
        public int Id { get; set; }
    }
}