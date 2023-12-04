using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mba.BooksLibrary.Model {
    public class Prestecs {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string LibraryId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public DateTime? DueDate { get; set; } = null!;
        public DateTime? ReturnedDate { get; set; } = null!;
    }
}