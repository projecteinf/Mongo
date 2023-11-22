using MongoDB.Bson;
using MongoDB.Driver;

static class CRUD {
    public static void Create(MongoClient mongoClient, Book book) {
        mongoClient.GetDatabase("test").GetCollection<BsonDocument>("test").InsertOne(book.ToBsonDocument());
    }

    public static void Read() {
        // ...
    }

    public static void Update() {
        // ...
    }

    public static void Delete() {
        // ...
    }
}