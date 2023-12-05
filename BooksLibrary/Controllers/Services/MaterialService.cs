using mba.BooksLibrary.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace mba.BooksLibrary.Services;

public class MaterialService
{
    private readonly IMongoCollection<Material> _materialCollection;
    private readonly IMongoCollection<Library> _libraryCollection;
    public MaterialService(
        IOptions<BooksLibraryDatabaseSettings> BooksLibraryDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            BooksLibraryDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            BooksLibraryDatabaseSettings.Value.DatabaseName);

        _materialCollection = mongoDatabase.GetCollection<Material>(
            BooksLibraryDatabaseSettings.Value.MaterialCollectionName);

        _libraryCollection = mongoDatabase.GetCollection<Library>(
            BooksLibraryDatabaseSettings.Value.LibraryCollectionName);
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
    public async Task<Material> AssignLibraryAsync(string id, string idLibrary) {
        Material material = await GetAsync(id) ?? throw new Exception("No existeix el llibre"); 
        Library library = await _libraryCollection.Find<Library>(x => x.Id == idLibrary).FirstOrDefaultAsync();
        
        // No interessa fer-ho d'aquesta forma ja que la llibreria conté tots els socis de la mateixa
        // Cal fer-ho amb ID de referència a la llibreria per tal que la col·lecció 
        // de materials no sobrepassi el límit de 16MB
        
        material.Library=library;
        await UpdateAsync(id, material);
        return material;
    }
    public async Task<List<Material>> NotReturnedAsync()
    {
        List<Material> materials = await GetAsync();
        List<Material> notReturned = new List<Material>();

        return materials.Where(x => x.prestecs is not null).Where(x => x.prestecs.Any(x => x.ReturnedDate is null)).ToList();
    }

    public async Task<List<Material>> GetNotReturnedByUser(string id)
    {
        List<Material> materials = await GetAsync();
        
        return materials.Where(x => x.prestecs is not null).Where(x => x.prestecs.Any(x => x.ReturnedDate is null && x.UserId == id)).ToList();
    }

    public async Task ReturnMaterialAsync(string id, string userId)
    {
        Material material = await GetAsync(id) ?? throw new Exception("No existeix el llibre");
        List<Prestecs> prestecs = material.prestecs.ToList();
        Prestecs prestec = prestecs.Where(x => x.UserId == userId && x.ReturnedDate is null).FirstOrDefault() ?? throw new Exception("No existeix el prestec");
        Console.WriteLine(prestec);
        prestec.ReturnedDate = DateTime.Now;
        await UpdateAsync(id, material);
    }

}