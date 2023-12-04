using MongoDB.Driver;

namespace mba.BooksLibrary {
    public static class MongoConnection
    {
        public static MongoClient connection = null;
        public static MongoClient GetConnection()
        {
            if (connection == null)
            {
                var mongoClientSettings = new MongoClientSettings {
                        Credential = MongoCredential.CreateCredential("admin", "root", "a"),
                        Server = new MongoServerAddress("localhost", 27017)
                };
                mongoClientSettings.AllowInsecureTls = true;
                connection = new MongoClient(mongoClientSettings);
            }
            return connection; 
        }
    }
}

    

