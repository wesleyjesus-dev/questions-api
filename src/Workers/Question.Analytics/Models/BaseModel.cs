using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Question.Analytics.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
    }
}
