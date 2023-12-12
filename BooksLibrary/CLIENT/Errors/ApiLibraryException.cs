using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace mba.BooksLibrary.Client.Exceptions {
    class ApiLibraryException : Exception {
        String Category,Message,CodeError;
        
        public ApiLibraryException() : base() { }
        public ApiLibraryException(string message) : base(message) { 
            
            object objectJson = Newtonsoft.Json.JsonConvert.DeserializeObject(message);
            var json = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(objectJson.ToString(), new { Category="", Code = 0, Errmsg = "" } );
            Console.WriteLine(json.Category + " - " + json.Code + " - " + json.Errmsg);

            Console.WriteLine(message);
        }
        public ApiLibraryException(string message,Exception innerException) : base(message,innerException) { 
            // A partir d'un objecte JSON, creem un objecte anònim amb les propietats que ens interessen i les assignem a un objecte anònim
            object objectJson = Newtonsoft.Json.JsonConvert.DeserializeObject(message);
            var json = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(objectJson.ToString(), new { Category="", Code = 0, Errmsg = "" } );
            Console.WriteLine(message + " - " + innerException.Message);
        }
    }
}