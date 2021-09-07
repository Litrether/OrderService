using MongoDB.Bson.Serialization.Attributes;

namespace OrderService.Data.Domain
{
    public abstract class KeyedEntityBase
    {
        [BsonId]
        public int Id { get; set; }
    }
}