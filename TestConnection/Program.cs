using System.Collections;
using System.Reflection;
using MongoDB.Driver;

void PrintProperties(object obj, string prefix = "")
{
    Type type = obj.GetType();
    PropertyInfo[] properties = type.GetProperties();

    foreach (PropertyInfo property in properties)
    {
        if (property!=null) {
            object propValue = property.GetValue(obj, null);
            if (propValue!=null) {
                Console.WriteLine($"{prefix}{property.Name} = {propValue}");
                Console.WriteLine();
                if (property.Name == "Databases") {
                    foreach (var db in (IEnumerable)propValue) {
                        PrintProperties(db, prefix + "  ");
                    }
                }
            }
        }
        
    }
}

MongoClient client = Connection.Connect();
Book book = new Book() {
    BookName = "The C# Programming Language",
    Price = 9.99m,
    Category = "Programming",
    Author = "Anders Hejlsberg"
};

CRUD.Create(client, book);
PrintProperties(client);



