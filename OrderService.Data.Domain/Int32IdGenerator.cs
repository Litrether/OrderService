using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace OrderService.Data.Core
{
    public class Int32IdGenerator<T> : IIdGenerator where T : class
    {
        private string _idCollectionName;

        public Int32IdGenerator()
        {
            _idCollectionName = "IDs";
        }

        public object GenerateId(object container, object document)
        {
            var idSequenceCollection = ((IMongoCollection<T>)container).Database
                 .GetCollection<BsonDocument>(_idCollectionName);
            var collectionName = document.GetType().Name;

            var filterQuery = Builders<BsonDocument>.Filter.Eq("_id", collectionName);
            var updates = Builders<BsonDocument>.Update.Inc("seq", 1);
            var updateOptions = new FindOneAndUpdateOptions<BsonDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var doc = idSequenceCollection.FindOneAndUpdate(filterQuery, updates, updateOptions);
            return ConvertToInt(doc["seq"]);
        }

        private object ConvertToInt(BsonValue value)
        {
            if (value.BsonType == BsonType.Int32)
                return value.AsInt32;

            return value.AsInt64;
        }

        public bool IsEmpty(object id)
        {
            return ((int)id) == 0;
        }
    }
}
