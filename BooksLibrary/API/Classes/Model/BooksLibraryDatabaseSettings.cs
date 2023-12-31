namespace mba.BooksLibrary.Model {
    public class BooksLibraryDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
        public string MaterialCollectionName { get; set; } = null!;
        public string LibraryCollectionName { get; set; } = null!;
    }
}