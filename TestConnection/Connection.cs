public static class Connection
{
    public static MongoDB.Driver.MongoClient Connect(string ConnectionString)
    {
        return new MongoDB.Driver.MongoClient(ConnectionString);
    }
    
}
