using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mba.BooksLibrary.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DNI { get; set; } = null!;
    }
}