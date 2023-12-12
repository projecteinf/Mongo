using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using mba.BooksLibrary.Client.Exceptions;
using mba.BooksLibrary.Model;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace mba.BooksLibrary.Client {
    class Program
    {
        const string APIRULBASE = "http://localhost:5050/api/v1/";
        const string APIRULLIBRARY = APIRULBASE + "Libraries";
        static async Task Main()
        {
            Library library = new Library();
            library.Name = "Gran d'Olot";
            
            await CreateLibrary(library);
            await GetAllLibraries();
        }

        static async Task CreateLibrary(Library library)
        {
            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = null;
                try {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(library), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync(APIRULLIBRARY, content);
                } 
                catch (HttpRequestException ex) { new ApiCallException("Error en la petició HTTP", ex); }
                catch (Exception ex) { new ApiCallException("Error ", ex); }

                if (response.IsSuccessStatusCode) {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Resposta de l'API: " + result);
                }
                else {
                    string result = await response.Content.ReadAsStringAsync();
                    new ApiLibraryException(result);
                }
                
            }
        }
        static async Task GetAllLibraries()
        {
            using (HttpClient client = new HttpClient()) {
                try {
                    HttpResponseMessage response = await client.GetAsync(APIRULLIBRARY);

                    if (response.IsSuccessStatusCode) {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta de l'API: " + content);
                    }
                    else throw new ApiLibraryException("Error en la petició HTTP. Ruta no vàlida. Codi d'estat: " + response.StatusCode);
                }
                catch (HttpRequestException ex)
                {
                    new ApiCallException("Error en la petició HTTP", ex);
                }
                catch (Exception ex)
                {
                    new ApiCallException("Error ", ex);
                }
            }
        }
    }
}