using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mba.BooksLibrary.Model {
    public class Library
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        public User[] Users { get; set; } = null!;
        
    }
}
