using MongoDB.Driver;

public static class Connection
{
    public static MongoDB.Driver.MongoClient Connect()
    {
        var mongoClientSettings = new MongoClientSettings
            {
                Credential = MongoCredential.CreateCredential("booksdb", "root", "a"),
                Server = new MongoServerAddress("localhost", 27017)
            };
        return new MongoDB.Driver.MongoClient(mongoClientSettings);
    }
    
}
