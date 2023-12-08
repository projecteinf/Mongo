using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace mba.BooksLibrary.Client.Exceptions {
    class ApiLibraryException : Exception {
        public ApiLibraryException() : base() { }
        public ApiLibraryException(string message) : base(message) { 
            int start = message.IndexOf("WriteError:");
            int end = message.IndexOf("}", start) + 1;
            string mongoErrorString = message.Substring(start, end - start);
            
            Exception ex = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(mongoErrorString);


            Console.WriteLine(message);
        }
        public ApiLibraryException(string message,Exception innerException) : base(message,innerException) { 
            Console.WriteLine(message + " - " + innerException.Message);
        }
    }
}