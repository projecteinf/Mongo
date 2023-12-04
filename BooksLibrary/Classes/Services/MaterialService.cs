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

    public async Task AfegirPrestecAsync(string id, Prestecs prestec)
    {
        Material material = await GetAsync(id) ?? throw new Exception("No existeix el llibre");
        List<Prestecs> prestecs;

        if (material.prestecs is null) prestecs = new List<Prestecs>();
        else prestecs = material.prestecs.ToList();
        
        prestecs.Add(prestec);
        material.prestecs = prestecs;
        await UpdateAsync(id, material);
    }

    public async Task<List<Material>> NotReturnedAsync()
    {
        List<Material> materials = await GetAsync();
        List<Material> notReturned = new List<Material>();

        return materials.Where(x => x.prestecs is not null).Where(x => x.prestecs.Any(x => x.ReturnedDate is null)).ToList();
    }

    public async Task ReturnMaterialAsync(string id, string userId)
    {
        Material material = await GetAsync(id) ?? throw new Exception("No existeix el llibre");
        List<Prestecs> prestecs = material.prestecs.ToList();
        Prestecs prestec = prestecs.Where(x => x.UserId == userId).FirstOrDefault();
        prestec.ReturnedDate = DateTime.Now;
        await UpdateAsync(id, material);
    }
}