using System;
using System.Net.Http;
using System.Threading.Tasks;
using mba.BooksLibrary.Client.Exceptions;

namespace mba.BooksLibrary.Client {
    class Program
    {
        static async Task Main()
        {
            await MakeRequest();
        }

        static async Task MakeRequest()
        {
            string apiUrl = "http://localhost:5050/api/v2/Library";

            using (HttpClient client = new HttpClient()) {
                try {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

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