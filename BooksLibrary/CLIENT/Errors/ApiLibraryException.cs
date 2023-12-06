namespace mba.BooksLibrary.Client.Exceptions {
    class ApiLibraryException : Exception {
        public ApiLibraryException() : base() { }
        public ApiLibraryException(string message) : base(message) { }
        public ApiLibraryException(string message,Exception innerException) : base(message,innerException) { 
            Console.WriteLine(message + " - " + innerException.Message);
        }
    }
}