using mba.BooksLibrary.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mba.BooksLibrary.Services;

public class LibraryService
{
    private readonly IMongoCollection<Library> _libraryCollection;

    public LibraryService(IOptions<BooksLibraryDatabaseSettings> BooksLibraryDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            BooksLibraryDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            BooksLibraryDatabaseSettings.Value.DatabaseName);

        _libraryCollection = mongoDatabase.GetCollection<Library>(
            BooksLibraryDatabaseSettings.Value.LibraryCollectionName);
    }

    public async Task<List<Library>> GetAsync() =>
        await _libraryCollection.Find(_ => true).ToListAsync();

    public async Task<Library?> GetAsync(string id) =>
        await _libraryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Library newLibrary) =>
        await _libraryCollection.InsertOneAsync(newLibrary);

    public async Task UpdateAsync(string id, Library updatedLibrary) =>
        await _libraryCollection.ReplaceOneAsync(x => x.Id == id, updatedLibrary);

    public async Task RemoveAsync(string id) =>
        await _libraryCollection.DeleteOneAsync(x => x.Id == id);

    public async Task AddUserAsync(string id, User user) {
        Library library = await GetAsync(id) ?? throw new Exception("No existeix la biblioteca");
        List<User> users;

        if (library.Users is null) users = new List<User>();
        else users = library.Users.ToList();
        
        users.Add(user);
        library.Users = users;
        await UpdateAsync(id, library);
    }
}