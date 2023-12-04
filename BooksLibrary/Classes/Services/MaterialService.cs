using mba.BooksLibrary.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mba.BooksLibrary.Services;

public class MaterialService
{
    private readonly IMongoCollection<Material> _materialCollection;

    public MaterialService(
        IOptions<BooksLibraryDatabaseSettings> BooksLibraryDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            BooksLibraryDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            BooksLibraryDatabaseSettings.Value.DatabaseName);

        _materialCollection = mongoDatabase.GetCollection<Material>(
            BooksLibraryDatabaseSettings.Value.MaterialCollectionName);
    }

    public async Task<List<Material>> GetAsync() =>
        await _materialCollection.Find(_ => true).ToListAsync();

    public async Task<Material?> GetAsync(string id) =>
        await _materialCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Material newMaterial) =>
        await _materialCollection.InsertOneAsync(newMaterial);

    public async Task UpdateAsync(string id, Material updatedMaterial) =>
        await _materialCollection.ReplaceOneAsync(x => x.Id == id, updatedMaterial);

    public async Task RemoveAsync(string id) =>
        await _materialCollection.DeleteOneAsync(x => x.Id == id);
}