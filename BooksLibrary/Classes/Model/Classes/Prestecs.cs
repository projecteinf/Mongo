namespace mba.BooksLibrary.Model {
    public class Prestecs {
        public string LibraryId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public DateTime? DueDate { get; set; } = null!;
        public DateTime? ReturnedDate { get; set; } = null!;
    }
}