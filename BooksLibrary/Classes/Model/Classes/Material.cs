using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mba.BooksLibrary.Model {
    public class Material
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        [BsonElement("Type")]
        public string Type { get; set; } = null!;
        public List<Prestecs>? prestecs { get; set; } = null!;

        public override string ToString() => $"{Id} {Name} ({Type})";
    }
}
