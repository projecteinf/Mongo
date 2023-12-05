using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mba.BooksLibrary.Model {
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("Name")]
        public string BookName { get; set; } = null!;
        public Prestecs[] prestecs { get; set; } = null!;
    }
}

